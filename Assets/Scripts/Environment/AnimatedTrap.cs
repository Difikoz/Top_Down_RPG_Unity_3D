using System;
using UnityEngine;

namespace WinterUniverse
{
    public class AnimatedTrap : MonoBehaviour
    {
        private DamageCollider _damageCollider;

        private void Awake()
        {
            _damageCollider = GetComponentInChildren<DamageCollider>();
            _damageCollider.Initialize();
        }

        public void OpenDamageCollider()
        {
            _damageCollider.EnableCollider();
        }

        public void CloseDamageCollider()
        {
            _damageCollider.DisableCollider();
        }

        public void ClearDamagedTargets()
        {
            _damageCollider.ClearTargets();
        }
    }
}