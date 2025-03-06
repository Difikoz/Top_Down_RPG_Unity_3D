using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        public Action OnTargetChanged;

        private PawnController _pawn;
        private PawnController _target;
        private RelationshipState _relationshipToTarget;
        private float _distanceToTarget;
        private float _angleToTarget;

        [SerializeField] private float _followDistance = 4f;

        public PawnController Target => _target;
        public RelationshipState RelationshipToTarget => _relationshipToTarget;
        public float DistanceToTarget => _distanceToTarget;
        public float AngleToTarget => _angleToTarget;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _pawn.Equipment.OnEquipmentChanged += OnEquipmentChanged;
        }

        public void ResetComponent()
        {
            _pawn.Equipment.OnEquipmentChanged -= OnEquipmentChanged;
        }

        public void OnUpdate()
        {
            if (_target != null)
            {
                if (_target.Status.IsDead)
                {
                    _pawn.Locomotion.SetDestination(transform.position);
                }
                else if (!_pawn.Animator.IsPerfomingAction)
                {
                    _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                    _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                    switch (_relationshipToTarget)
                    {
                        case RelationshipState.Enemy:
                            if (_pawn.Equipment.WeaponSlot.Config != null && _distanceToTarget <= _pawn.Equipment.WeaponSlot.Config.AttackRange && Mathf.Abs(_angleToTarget) <= _pawn.Equipment.WeaponSlot.Config.AttackAngle / 2f)
                            {
                                _pawn.Animator.PlayAction("Attack");
                            }
                            break;
                        case RelationshipState.Neutral:
                            // simple follow
                            break;
                        case RelationshipState.Ally:
                            if (_target.Combat.Target != null && _target.Combat.RelationshipToTarget == RelationshipState.Enemy)
                            {
                                SetTarget(Target.Combat.Target);
                            }
                            break;
                    }
                }
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null && !target.Status.IsDead)
            {
                _target = target;
                _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                _relationshipToTarget = _pawn.Faction.Config.GetState(target.Faction.Config);
                if (_relationshipToTarget == RelationshipState.Enemy && _pawn.Equipment.WeaponSlot.Config != null)
                {
                    _pawn.Locomotion.SetTarget(_target.transform, _pawn.Equipment.WeaponSlot.Config.AttackRange / 2f);
                }
                else
                {
                    _pawn.Locomotion.SetTarget(_target.transform, _followDistance);
                }
            }
            else
            {
                _target = null;
                _distanceToTarget = 0f;
                _angleToTarget = 0f;
                _pawn.Locomotion.SetTarget(null);
            }
            OnTargetChanged?.Invoke();
        }

        private void OnEquipmentChanged()
        {
            if (_target != null)
            {
                if (_relationshipToTarget == RelationshipState.Enemy && _pawn.Equipment.WeaponSlot.Config != null)
                {
                    _pawn.Locomotion.SetTarget(_target.transform, _pawn.Equipment.WeaponSlot.Config.AttackRange / 2f);
                }
                else
                {
                    _pawn.Locomotion.SetTarget(_target.transform, _followDistance);
                }
            }
        }
    }
}