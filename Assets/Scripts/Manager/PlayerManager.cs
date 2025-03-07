using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PawnConfig _config;

        private PawnController _pawn;
        private Vector2 _cursorLocalPosition;
        private Vector3 _cursorWorldPosition;
        private Ray _cameraRay;
        private RaycastHit _cameraHit;

        public PawnController Pawn => _pawn;

        public void OnCursorPosition(InputValue value)
        {
            _cursorLocalPosition = value.Get<Vector2>();
        }

        public void OnLeftClick()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                _cursorWorldPosition = _cameraHit.point;
                InteractableBase interactable = _cameraHit.transform.GetComponentInParent<InteractableBase>();
                if (interactable != null && Vector3.Distance(_pawn.transform.position, interactable.PointToInteract.position) <= interactable.DistanceToInteract && interactable.CanInteract(_pawn))
                {
                    interactable.Interact(_pawn);
                }
                else if (_cameraHit.transform.TryGetComponent(out interactable) && Vector3.Distance(_pawn.transform.position, interactable.PointToInteract.position) <= interactable.DistanceToInteract && interactable.CanInteract(_pawn))
                {
                    interactable.Interact(_pawn);
                }
                else if (_cameraHit.transform.TryGetComponent(out PawnController pawn) && pawn != _pawn && pawn.StateHolder.CheckStateValue("Is Dead", false))
                {
                    if (pawn == _pawn.Combat.Target)
                    {
                        _pawn.Combat.SetTarget(pawn);
                    }
                    else
                    {
                        _pawn.Combat.SetTarget(pawn, false);
                    }
                }
                else
                {
                    _pawn.Locomotion.SetDestination(_cursorWorldPosition);
                }
            }
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