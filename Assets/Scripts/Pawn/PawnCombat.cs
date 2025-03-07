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
        private Vector3 _directionToTarget;
        private float _distanceToTarget;
        private float _angleToTarget;

        [SerializeField] private float _followDistance = 4f;

        public PawnController Target => _target;
        public RelationshipState RelationshipToTarget => _relationshipToTarget;
        public Vector3 DirectionToTarget => _directionToTarget;
        public float DistanceToTarget => _distanceToTarget;
        public float AngleToTarget => _angleToTarget;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _pawn.Equipment.OnEquipmentChanged += FollowTarget;
        }

        public void ResetComponent()
        {
            _pawn.Equipment.OnEquipmentChanged -= FollowTarget;
        }

        public void OnUpdate()
        {
            if (_target != null)
            {
                if (_target.StateHolder.CompareStateValue("Is Dead", true))
                {
                    _pawn.Locomotion.SetDestination(transform.position);
                }
                else if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", false))
                {
                    _directionToTarget = (_target.transform.position - transform.position).normalized;
                    _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                    _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                    if (_pawn.Equipment.WeaponSlot.Config != null && _pawn.StateHolder.CompareStateValue("Is Attacking", true) && _distanceToTarget <= _pawn.Equipment.WeaponSlot.Config.AttackRange && Mathf.Abs(_angleToTarget) <= _pawn.Equipment.WeaponSlot.Config.AttackAngle / 2f)
                    {
                        _pawn.Animator.PlayAction("Attack");
                    }
                    //if (_target.transform != _pawn.Locomotion.FollowTarget)
                    //{
                    //    return;
                    //}
                    //switch (_relationshipToTarget)
                    //{
                    //    case RelationshipState.Enemy:
                    //        if (_pawn.Equipment.WeaponSlot.Config != null && _distanceToTarget <= _pawn.Equipment.WeaponSlot.Config.AttackRange && Mathf.Abs(_angleToTarget) <= _pawn.Equipment.WeaponSlot.Config.AttackAngle / 2f)
                    //        {
                    //            _pawn.Animator.PlayAction("Attack");
                    //        }
                    //        break;
                    //    case RelationshipState.Neutral:
                    //        // simple follow
                    //        break;
                    //    case RelationshipState.Ally:
                    //        if (_target.Combat.Target != null && _target.Combat.RelationshipToTarget == RelationshipState.Enemy)
                    //        {
                    //            SetTarget(Target.Combat.Target);
                    //        }
                    //        break;
                    //}
                }
            }
        }

        public void SetTarget(PawnController target, bool follow = true, bool attack = false)
        {
            if (target != null && target.StateHolder.CompareStateValue("Is Dead", false))
            {
                _target = target;
                _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                _relationshipToTarget = _pawn.Faction.Config.GetState(target.Faction.Config);
                if (follow)
                {
                    FollowTarget();
                }
                else
                {
                    _pawn.Locomotion.SetTarget(null);
                }
            }
            else
            {
                _target = null;
                _distanceToTarget = 0f;
                _angleToTarget = 0f;
                _pawn.Locomotion.SetTarget(null);
            }
            _pawn.StateHolder.SetState("Is Attacking", attack);
            OnTargetChanged?.Invoke();
        }

        private void FollowTarget()
        {
            if (_relationshipToTarget == RelationshipState.Enemy && _pawn.Equipment.WeaponSlot.Config != null)
            {
                _pawn.Locomotion.SetTarget(_target.transform, _pawn.Equipment.WeaponSlot.Config.AttackRange / 2f, _pawn.Equipment.WeaponSlot.Config.AttackRange);
            }
            else
            {
                _pawn.Locomotion.SetTarget(_target.transform, _followDistance / 2f, _followDistance * 2f);
            }
        }

        //public bool GetEnemyTarget(out PawnController enemy)
        //{
        //    enemy = _target;
        //    return enemy != null && _relationshipToTarget == RelationshipState.Enemy;
        //}
    }
}