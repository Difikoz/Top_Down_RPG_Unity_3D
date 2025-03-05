using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private NavMeshAgent _agent;
        private CapsuleCollider _collider;
        private Transform _target;
        private Vector3 _destination;
        private float _remainingDistance;
        private float _velocity;
        private float _updateDestinationTime;
        private bool _reachedDestination;

        [SerializeField] private float _updateDestinationCooldown = 1f;
        [SerializeField] private bool _pickRandomPointAround;
        [SerializeField] private float _randomPointDistance = 10f;

        public NavMeshAgent Agent => _agent;
        public float Velocity => _velocity;
        public bool ReachedDestination => _reachedDestination;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<CapsuleCollider>();
            _agent.agentTypeID = _pawn.Animator.AgentTypeID;
            _agent.height = _pawn.Animator.Height;
            _agent.radius = _pawn.Animator.Radius;
            _collider.height = _agent.height;
            _collider.radius = _agent.radius;
            _collider.center = new(0f, _agent.height / 2f, 0f);
            _collider.isTrigger = true;
            _pawn.Status.OnStatsChanged += OnStatsChanged;
        }

        public void ResetComponent()
        {
            _pawn.Status.OnStatsChanged -= OnStatsChanged;
        }

        public void OnUpdate()
        {
            if (_updateDestinationTime >= _updateDestinationCooldown)
            {
                if (_target != null)
                {
                    SetDestination(_target.position, false);
                }
                _updateDestinationTime = 0f;
                if (_pickRandomPointAround)
                {
                    SetDestinationAroundSelf(0f, _randomPointDistance);
                }
            }
            else
            {
                _updateDestinationTime += Time.deltaTime;
            }
            if (_reachedDestination && _remainingDistance > 1f)
            {
                StartMovement();
            }
            else if (!_reachedDestination && _remainingDistance < 1f)
            {
                StopMovement();
            }
            _remainingDistance = Vector3.Distance(transform.position, _destination);
            _velocity = _agent.velocity.magnitude / _agent.speed;
        }

        private void OnStatsChanged()
        {
            _agent.acceleration = _pawn.Animator.Acceleration * _pawn.Status.Acceleration.CurrentValue / 100f;
            _agent.speed = _pawn.Animator.MoveSpeed * _pawn.Status.MoveSpeed.CurrentValue / 100f;
            _agent.angularSpeed = _pawn.Animator.RotateSpeed * _pawn.Status.RotateSpeed.CurrentValue / 100f;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetDestination(Vector3 position, bool resetTarget = true)
        {
            if (resetTarget)
            {
                SetTarget(null);
            }
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                _destination = hit.position;
                StartMovement();
            }
        }

        public void SetDestinationAroundPoint(Vector3 position, float minRange, float maxRange)
        {
            if (Random.value > 0.5f)
            {
                position.x += Random.Range(minRange, maxRange);
            }
            else
            {
                position.x -= Random.Range(minRange, maxRange);
            }
            if (Random.value > 0.5f)
            {
                position.z += Random.Range(minRange, maxRange);
            }
            else
            {
                position.z -= Random.Range(minRange, maxRange);
            }
            SetDestination(position);
        }

        public void SetDestinationAroundSelf(float minRange, float maxRange)
        {
            Vector3 position = transform.position;
            if (Random.value > 0.5f)
            {
                position.x += Random.Range(minRange, maxRange);
            }
            else
            {
                position.x -= Random.Range(minRange, maxRange);
            }
            if (Random.value > 0.5f)
            {
                position.z += Random.Range(minRange, maxRange);
            }
            else
            {
                position.z -= Random.Range(minRange, maxRange);
            }
            SetDestination(position);
        }

        public void StartMovement()
        {
            _agent.SetDestination(_destination);
            _reachedDestination = false;
        }

        public void StopMovement()
        {
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}