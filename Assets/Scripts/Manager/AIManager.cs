using UnityEngine;

namespace WinterUniverse
{
    public class AIManager : MonoBehaviour
    {
        private PawnController _pawn;

        [SerializeField] private PawnConfig _config;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(Vector3.right, Quaternion.identity);
            _pawn.Initialize(_config);
        }

        public void ResetComponent()
        {
            _pawn.ResetComponent();
        }

        public void OnUpdate()
        {
            _pawn.OnUpdate();
        }
    }
}