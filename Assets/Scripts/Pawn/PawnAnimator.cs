using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnUpdate()
        {
            _animator.SetFloat("Velocity", _pawn.Velocity);
        }
    }
}