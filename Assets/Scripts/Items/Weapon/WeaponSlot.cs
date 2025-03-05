using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;

        public WeaponItemConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                _pawn.Status.RemoveStatModifiers(_config.Modifiers);
                Destroy(_model);
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.Modifiers);
                // spawn model
                // initialize Damage Collider
            }
        }
    }
}