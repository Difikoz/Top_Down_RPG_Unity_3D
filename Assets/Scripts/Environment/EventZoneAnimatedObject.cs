using UnityEngine;

namespace WinterUniverse
{
    public class EventZoneAnimatedObject : EventZoneBase
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _animationSpeed = 1f;

        protected override void OnEntered(PawnController target)
        {
            if (_enteredTargets.Count > 0)
            {
                return;
            }
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
        }
    }
}