using UnityEngine;

namespace WinterUniverse
{
    public class EventZoneAnimatedTrap : EventZoneAnimatedObject
    {
        [SerializeField] private float _cooldown = 2f;
        [SerializeField] private float _deactivateTime = 1f;

        private AnimatedTrap _trap;
        private float _activatedTime;

        private void Awake()
        {
            _trap = GetComponentInChildren<AnimatedTrap>();
        }

        protected override void OnEntered(PawnController target)
        {
            if (_enteredTargets.Count > 0 || Time.time < _activatedTime + _cooldown)
            {
                return;
            }
            ActivateTrap();
            if (_deactivateTime > 0f)
            {
                Invoke(nameof(DeactivateTrap), _deactivateTime);
            }
        }

        protected override void OnExited(PawnController target)
        {
            if (_deactivateTime == 0f && _enteredTargets.Count > 0)
            {
                return;
            }
            DeactivateTrap();
        }

        private void ActivateTrap()
        {
            _activatedTime = Time.time;
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", true);
        }

        private void DeactivateTrap()
        {
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", false);
        }
    }
}