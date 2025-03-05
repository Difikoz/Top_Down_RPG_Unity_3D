using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ArmorItemConfig : ItemConfig
    {
        [SerializeField] private ArmorTypeConfig _armorType;
        [SerializeField] private float _resistance = 5f;

        public ArmorTypeConfig ArmorType => _armorType;
        public float Resistance => _resistance;
    }
}