using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private ArmorTypeConfig _type;
        [SerializeField] private List<ArmorRenderer> _armorRenderes = new();

        private PawnController _pawn;
        private ArmorItemConfig _config;

        public ArmorItemConfig Config => _config;
        public ArmorTypeConfig Type => _type;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(ArmorItemConfig config)
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
            foreach (ArmorRenderer ar in _armorRenderes)
            {
                ar.Toggle(ar.Config == _config);
            }
        }
    }
}