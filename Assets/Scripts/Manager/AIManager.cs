using UnityEngine;

namespace WinterUniverse
{
    public class AIManager : MonoBehaviour
    {
        private PawnController _pawn;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = GetComponentInChildren<PawnController>();// change to spawn from default
            _pawn.transform.SetParent(null);
            _pawn.Initialize();
        }

        public void OnUpdate()
        {
            _pawn.OnUpdate();
        }
    }
}