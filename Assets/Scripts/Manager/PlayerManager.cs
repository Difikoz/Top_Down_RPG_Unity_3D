using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PawnConfig _config;

        private PawnController _pawn;
        private Vector2 _cursorLocalPosition;
        private Ray _cameraRay;
        private RaycastHit _cameraHit;

        public PawnController Pawn => _pawn;

        public void OnCursorPosition(InputValue value)
        {
            _cursorLocalPosition = value.Get<Vector2>();
        }

        public void OnMoveToPosition()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                _pawn.Locomotion.StopMovement();
                _pawn.Locomotion.SetDestination(_cameraHit.point);
            }
        }

        public void OnInteract()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                InteractableBase interactable = _cameraHit.transform.GetComponentInParent<InteractableBase>();
                if (interactable != null)
                {
                    _pawn.Locomotion.SetDestination(interactable);
                }
                else if (_cameraHit.transform.TryGetComponent(out interactable))
                {
                    _pawn.Locomotion.SetDestination(interactable);
                }
            }
        }

        public void OnLockTarget()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                if (_cameraHit.transform.TryGetComponent(out PawnController pawn) && pawn != _pawn)
                {
                    _pawn.Combat.SetTarget(pawn);
                }
            }
        }

        public void OnFollowSelectedTarget()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _pawn.Combat.FollowTarget();
        }

        public void OnAttackSelectedTarget()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _pawn.Combat.AttackTarget();
        }

        public void OnResetSelectedTarget()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _pawn.Combat.SetTarget(null);
        }

        public void Initialize(PawnConfig config)
        {
            _config = config;
            Initialize();
        }

        public void Initialize()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(Vector3.zero, Quaternion.identity);// get transform from save data
            _pawn.Initialize(_config);
        }

        public void ResetComponent()
        {
            _pawn.ResetComponent();
        }

        public void OnUpdate()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorLocalPosition);
            _pawn.OnUpdate();
            //transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
        }
    }
}