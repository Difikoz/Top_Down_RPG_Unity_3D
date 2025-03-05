using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;

        public WeaponItemConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                // remove stat modifiers
            }
            _config = config;
            if (_config != null)
            {
                // add stat modifiers
            }
            // initialize Damage Collider
        }
    }
}