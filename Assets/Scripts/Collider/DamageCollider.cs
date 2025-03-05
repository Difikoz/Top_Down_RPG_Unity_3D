using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        private Collider _collider;
        private PawnController _owner;
        private List<PawnController> _damagedTargets = new();

        [SerializeField] private List<DamageType> _damageTypes = new();

        public void Initialize(PawnController owner, List<DamageType> damageTypes)
        {
            _owner = owner;
            _damageTypes = new(damageTypes);
            DisableCollider();
        }

        public void EnableCollider()
        {
            _collider.enabled = true;
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
            _damagedTargets.Clear();
        }

        private void HitTarget(PawnController target)
        {
            foreach (DamageType dt in _damageTypes)
            {
                target.Status.ReduceHealthCurrent(dt.Damage, dt.Type, _owner);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (target != null && target != _owner)
            {
                _damagedTargets.Add(target);
                HitTarget(target);
            }
        }
    }
}