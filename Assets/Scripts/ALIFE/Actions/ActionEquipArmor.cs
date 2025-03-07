using UnityEngine;

namespace WinterUniverse
{
    public class ActionEquipArmor : ActionBase
    {
        [SerializeField] private ArmorTypeConfig _armorType;
        private ArmorItemConfig _armor;

        public override bool CanStart()
        {
            if (_npc.Pawn.Inventory.GetArmor(_armorType.DisplayName, out _armor))
            {
                return base.CanStart();
            }
            return false;
        }

        public override void OnStart()
        {
            base.OnStart();
            _armor.Use(_npc.Pawn);
            _armor = null;
        }
    }
}