using UnityEngine;

namespace WinterUniverse
{
    public class EventZoneAnimatedTrap : EventZoneAnimatedObject
    {
        private DamageCollider _damageCollider;

        private void Awake()
        {
            _damageCollider = GetComponentInChildren<DamageCollider>();
            _damageCollider.Initialize();
        }

        protected override void OnEntered(PawnController target)
        {
            if (_enteredTargets.Count > 0)
            {
                return;
            }
            _damageCollider.EnableCollider();
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", true);
        }

        protected override void OnExited(PawnController target)
        {
            if (_enteredTargets.Count > 0)
            {
                return;
            }
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", false);
            _damageCollider.DisableCollider();
        }
    }
}