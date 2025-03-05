using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private WeaponTypeConfig _weaponType;
        [SerializeField] private List<DamageType> _damageTypes = new();

        public WeaponTypeConfig WeaponType => _weaponType;
        public List<DamageType> DamageTypes => _damageTypes;
    }
}