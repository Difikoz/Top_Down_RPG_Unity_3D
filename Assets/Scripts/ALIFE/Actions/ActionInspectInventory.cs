using UnityEngine;

namespace WinterUniverse
{
    public class ActionInspectInventory : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            if (_npc.Pawn.Inventory.GetWeapon(out WeaponItemConfig weapon))
            {
                if (_npc.Pawn.Equipment.WeaponSlot.Config == null || _npc.Pawn.Equipment.WeaponSlot.Config.Price < weapon.Price)
                {
                    _npc.Pawn.StateHolder.SetState("Has Best Weapon", true);
                }
            }
            foreach (ArmorSlot slot in _npc.Pawn.Equipment.ArmorSlots)
            {
                if (_npc.Pawn.Inventory.GetArmor(slot.Type.DisplayName, out ArmorItemConfig armor))
                {
                    if (slot.Config == null || slot.Config.Price < armor.Price)
                    {
                        _npc.Pawn.StateHolder.SetState($"Has Best {slot.Type.DisplayName}", true);
                    }
                }
            }
        }
    }
}