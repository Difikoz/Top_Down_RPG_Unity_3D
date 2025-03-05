using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private Vector2 _cursorLocalPosition;
        private Vector3 _cursorWorldPosition;

        public PawnController Pawn => _pawn;

        public void OnCursorPosition(InputValue value)
        {
            _cursorLocalPosition = value.Get<Vector2>();
        }

        public void OnLeftClick()
        {
            //_pawn.Locomotion.SetDestination(_cursorWorldPosition);
        }

        public void Initialize()
        {
            _pawn = GetComponentInChildren<PawnController>();// change to spawn from default
            _pawn.transform.SetParent(null);
            _pawn.Initialize();
        }

        public void OnUpdate()
        {
            // raycast to get cursor world point
            _pawn.OnUpdate();
        }
    }
}