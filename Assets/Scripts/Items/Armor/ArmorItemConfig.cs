using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ArmorItemConfig : ItemConfig
    {
        [SerializeField] private ArmorTypeConfig _armorType;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public ArmorTypeConfig ArmorType => _armorType;
        public List<StatModifierCreator> Modifiers => _modifiers;

        public override void Use(PawnController pawn)
        {
            pawn.Equipment.EquipArmor(this);
        }
    }
}