using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField, Range(0, 1)] private int _agentTypeID = 0;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private float _rotationSpeed = 180f;

        public int AgentTypeID => _agentTypeID;
        public float Height => _height;
        public float Radius => _radius;
        public float Acceleration => _acceleration;
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnUpdate()
        {
            _animator.SetFloat("Velocity", _pawn.Locomotion.Velocity);
        }
    }
}