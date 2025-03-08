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
        private InteractableBase _interactable;
        private float _velocity;
        private float _remainingDistance;
        private float _updateDestinationTime;
        private bool _reachedDestination;

        [SerializeField] private float _updateDestinationCooldown = 1f;
        [SerializeField] private float _minStopDistance = 1f;
        [SerializeField] private float _maxStopDistance = 2f;

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
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) && !_reachedDestination)
            {
                StopMovement();
                return;
            }
            else if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (_updateDestinationTime >= _updateDestinationCooldown)
            {
                if (_interactable != null)
                {
                    if (_remainingDistance <= _interactable.DistanceToInteract)
                    {
                        if (_interactable.CanInteract(_pawn))
                        {
                            _interactable.Interact(_pawn);
                        }
                        StopMovement();
                    }
                    else
                    {
                        SetDestination(_interactable.PointToInteract.position);
                    }
                }
                else if (_followTarget != null)
                {
                    SetDestination(_followTarget.position);
                }
                else
                {
                    SetFollowDistance();
                }
                _updateDestinationTime = 0f;
            }
            else
            {
                _updateDestinationTime += Time.deltaTime;
            }
            if (_reachedDestination && _remainingDistance > _maxStopDistance)
            {
                StartMovement();
            }
            else if (!_reachedDestination && _remainingDistance <= _minStopDistance)
            {
                StopMovement(false);
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

        public void SetFollowDistance(float minDistance = -1f, float maxDistance = -1f)
        {
            if (minDistance > 0f)
            {
                _minStopDistance = minDistance;
            }
            else
            {
                _minStopDistance = _agent.radius / 2f;
            }
            if (maxDistance > 0f)
            {
                _maxStopDistance = maxDistance;
            }
            else
            {
                _maxStopDistance = _agent.radius;
            }
            if (_maxStopDistance < _minStopDistance)
            {
                _maxStopDistance = _minStopDistance;
            }
        }

        public void SetDestination(Transform target)
        {
            _followTarget = target;
            SetDestination(_followTarget.position);
        }

        public void SetDestination(InteractableBase interactable)
        {
            _interactable = interactable;
            SetDestination(_interactable.PointToInteract.position);
        }

        public void SetDestination(Vector3 position)
        {
            for (int i = 1; i < 5; i++)
            {
                if (NavMesh.SamplePosition(position, out NavMeshHit hit, i * 5f, NavMesh.AllAreas))
                {
                    _destination = hit.position;
                    _remainingDistance = Vector3.Distance(transform.position, _destination);
                    StartMovement();
                    break;
                }
            }
        }

        public void SetDestinationAroundSelf(float minRange, float maxRange)
        {
            SetDestinationInRange(transform.position, minRange, maxRange);
        }

        public void SetDestinationAroundSelf(float radius)
        {
            SetDestinationInRange(transform.position, radius);
        }

        public void SetDestinationInRange(Vector3 position, float minRange, float maxRange)
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

        public void SetDestinationInRange(Vector3 position, float radius)
        {
            radius /= 2f;
            position += Vector3.right * Random.Range(-radius, radius);
            position += Vector3.forward * Random.Range(-radius, radius);
            SetDestination(position);
        }

        public void StartMovement()
        {
            if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            _agent.SetDestination(_destination);
            _reachedDestination = false;
        }

        public void StopMovement(bool resetTarget = true)
        {
            if (resetTarget)
            {
                _followTarget = null;
                _interactable = null;
                SetFollowDistance();
            }
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}