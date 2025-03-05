using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private NavMeshAgent _agent;
        private float _velocity;
        private bool _reachedDestination;

        public NavMeshAgent Agent => _agent;
        public float Velocity => _velocity;
        public bool ReachedDestination => _reachedDestination;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.agentTypeID = _pawn.Animator.AgentTypeID;
            _agent.height = _pawn.Animator.Height;
            _agent.radius = _pawn.Animator.Radius;
            _agent.acceleration = _pawn.Animator.Acceleration;
            _agent.speed = _pawn.Animator.MovementSpeed;
            _agent.angularSpeed = _pawn.Animator.RotationSpeed;
        }

        public void OnUpdate()
        {
            if (!_reachedDestination && _agent.remainingDistance < 1f)
            {
                StopMovement();
            }
            _velocity = _agent.velocity.magnitude / _agent.speed;
        }

        public void SetDestination(Vector3 position)
        {
            StopMovement();
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                _reachedDestination = false;
            }
        }

        public void StopMovement()
        {
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}