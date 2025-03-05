using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
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
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                _cursorWorldPosition = _cameraHit.point;
                if (_cameraHit.transform.TryGetComponent(out PawnController pawn) && pawn != _pawn)
                {
                    _pawn.Locomotion.SetTarget(pawn.transform);
                }
                else if (_cameraHit.transform.TryGetComponent(out InteractableBase interactable) && Vector3.Distance(_pawn.transform.position, interactable.PointToInteract.position) <= interactable.DistanceToInteract)
                {
                    if (interactable.CanInteract(_pawn))
                    {
                        interactable.Interact(_pawn);
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
            _pawn = GetComponentInChildren<PawnController>();// change to spawn from default
            _pawn.transform.SetParent(null);
            _pawn.Initialize();
        }

        public void OnUpdate()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorLocalPosition);
            _pawn.OnUpdate();
        }
    }
}