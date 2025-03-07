using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private NavMeshAgent _agent;
        private Rigidbody _rb;
        private CapsuleCollider _collider;
        private Transform _followTarget;
        private Vector3 _destination;
        private float _minFollowDistance;
        private float _maxFollowDistance;
        private float _velocity;
        private float _remainingDistance;
        private float _updateDestinationTime;
        private bool _reachedDestination;

        [SerializeField] private float _updateDestinationCooldown = 1f;
        [SerializeField] private float _basicMinFollowDistance = 2f;
        [SerializeField] private float _basicMaxFollowDistance = 4f;

        public NavMeshAgent Agent => _agent;
        public Transform FollowTarget => _followTarget;
        public float Velocity => _velocity;
        public float RemainingDistance => _remainingDistance;
        public bool ReachedDestination => _reachedDestination;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _agent.agentTypeID = _pawn.Animator.AgentTypeID;
            _agent.height = _pawn.Animator.Height;
            _agent.radius = _pawn.Animator.Radius;
            _rb.isKinematic = true;
            _collider.height = _agent.height;
            _collider.radius = _agent.radius;
            _collider.center = new(0f, _agent.height / 2f, 0f);
            _collider.isTrigger = true;
            _pawn.Status.OnStatsChanged += OnStatsChanged;
            _pawn.Status.OnDied += OnDied;
            _pawn.Status.OnRevived += OnRevived;
            _destination = transform.position;
        }

        public void ResetComponent()
        {
            _pawn.Status.OnStatsChanged -= OnStatsChanged;
            _pawn.Status.OnDied -= OnDied;
            _pawn.Status.OnRevived -= OnRevived;
        }

        public void OnUpdate()
        {
            _velocity = _agent.velocity.magnitude / _agent.speed;
            if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", true) || _pawn.StateHolder.CompareStateValue("Is Dead", true))
            {
                if (!_reachedDestination)
                {
                    StopMovement(true);
                }
                return;
            }
            if (_updateDestinationTime >= _updateDestinationCooldown)
            {
                if (_followTarget != null)
                {
                    SetDestination(_followTarget.position, false);
                }
                _updateDestinationTime = 0f;
            }
            else
            {
                _updateDestinationTime += Time.deltaTime;
            }
            if (_reachedDestination)
            {
                if (_remainingDistance > _maxFollowDistance)
                {
                    StartMovement();
                }
                else if (Mathf.Abs(_pawn.Combat.AngleToTarget) > 5f)
                {
                    transform.Rotate(Vector3.up * Mathf.Clamp(_pawn.Combat.AngleToTarget, -1f, 1f) * _agent.angularSpeed * Time.deltaTime);
                }
            }
            else if (!_reachedDestination && _remainingDistance < _minFollowDistance)
            {
                StopMovement();
            }
            _remainingDistance = Vector3.Distance(transform.position, _destination);
        }

        private void OnStatsChanged()
        {
            _agent.acceleration = _pawn.Animator.Acceleration * _pawn.Status.Acceleration.CurrentValue / 100f;
            _agent.speed = _pawn.Animator.MoveSpeed * _pawn.Status.MoveSpeed.CurrentValue / 100f;
            _agent.angularSpeed = _pawn.Animator.RotateSpeed * _pawn.Status.RotateSpeed.CurrentValue / 100f;
        }

        private void OnDied()
        {

        }

        private void OnRevived()
        {

        }

        public void SetTarget(Transform target, float minDistance = -1f, float maxDistance = -1f)
        {
            if (target != null)
            {
                _followTarget = target;
                if (minDistance == -1f)
                {
                    _minFollowDistance = _basicMinFollowDistance;
                }
                else
                {
                    _minFollowDistance = minDistance;
                }
                if (maxDistance == -1f)
                {
                    _maxFollowDistance = _basicMaxFollowDistance;
                }
                else
                {
                    _maxFollowDistance = maxDistance;
                }
            }
            else
            {
                _followTarget = null;
                _minFollowDistance = 0.1f;
                _maxFollowDistance = 0.2f;
            }
        }

        public void SetDestination(Vector3 position, bool resetTarget = true)
        {
            if (resetTarget)
            {
                _pawn.Combat.SetTarget(null);
            }
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                _destination = hit.position;
                StartMovement();
            }
        }

        public void SetDestinationAroundSelf(float minRange, float maxRange, bool resetTarget = true)
        {
            SetDestinationInRange(transform.position, minRange, maxRange, resetTarget);
        }

        public void SetDestinationAroundSelf(float radius, bool resetTarget = true)
        {
            SetDestinationInRange(transform.position, radius, resetTarget);
        }

        public void SetDestinationInRange(Vector3 position, float minRange, float maxRange, bool resetTarget = true)
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
            SetDestination(position, resetTarget);
        }

        public void SetDestinationInRange(Vector3 position, float radius, bool resetTarget = true)
        {
            radius /= 2f;
            position += Vector3.right * Random.Range(-radius, radius);
            position += Vector3.forward * Random.Range(-radius, radius);
            SetDestination(position, resetTarget);
        }

        public void StartMovement()
        {
            _agent.SetDestination(_destination);
            _reachedDestination = false;
        }

        public void StopMovement(bool resetTarget = false)
        {
            if (resetTarget)
            {
                SetTarget(null);
            }
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}