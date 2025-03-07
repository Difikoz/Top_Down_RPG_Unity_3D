using UnityEngine;

namespace WinterUniverse
{
    public class NPCController : MonoBehaviour
    {
        private PawnController _pawn;

        [SerializeField] private PawnConfig _config;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Initialize(_config);
        }

        public void ResetComponent()
        {
            _pawn.ResetComponent();
        }

        public void OnUpdate()
        {
            _pawn.OnUpdate();
            //transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            if (_pawn.Combat.Target == null && _pawn.Detection.DetectedPawns.Count > 0)
            {
                _pawn.Combat.SetTarget(_pawn.Detection.GetClosestPawn());
            }
        }
    }
}