using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;
        private DamageCollider _damageCollider;

        public WeaponItemConfig Config => _config;
        public DamageCollider DamageCollider => _damageCollider;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                _pawn.Status.RemoveStatModifiers(_config.Modifiers);
                LeanPool.Despawn(_model);
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.Modifiers);
                // spawn model
                _damageCollider = GetComponentInChildren<DamageCollider>();
                _damageCollider.Initialize(_pawn, _config.DamageTypes);
            }
        }
    }
}