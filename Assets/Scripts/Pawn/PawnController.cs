using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _animator;
        private NavMeshAgent _agent;
        private float _velocity;
        private bool _reachedDestination;

        public float Velocity => _velocity;
        public bool ReachedDestination => _reachedDestination;

        public void Initialize()
        {
            // spawn model
            _animator = GetComponentInChildren<PawnAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = 4f;// promote to stat
            _agent.angularSpeed = 180f;// promote to stat
        }

        public void OnUpdate()
        {
            if (!_reachedDestination && _agent.remainingDistance < 1f)
            {
                StopMovement();
            }
            _velocity = _agent.velocity.magnitude / _agent.speed;
            _animator.OnUpdate();
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