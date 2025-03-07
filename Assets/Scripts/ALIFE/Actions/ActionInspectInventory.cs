using UnityEngine;

namespace WinterUniverse
{
    public class ActionInspectInventory : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            if (_npc.Pawn.Inventory.GetWeapon(out WeaponItemConfig weapon) && (_npc.Pawn.Equipment.WeaponSlot.Config == null || _npc.Pawn.Equipment.WeaponSlot.Config.Price < weapon.Price))
            {
                _npc.Pawn.StateHolder.SetState("Has Best Weapon", true);
            }
            else
            {
                _npc.Pawn.StateHolder.SetState("Has Best Weapon", false);
            }
            foreach (ArmorSlot slot in _npc.Pawn.Equipment.ArmorSlots)
            {
                if (_npc.Pawn.Inventory.GetArmor(slot.Type.DisplayName, out ArmorItemConfig armor) && (slot.Config == null || slot.Config.Price < armor.Price))
                {
                    _npc.Pawn.StateHolder.SetState($"Has Best {slot.Type.DisplayName}", true);
                }
                else
                {
                    _npc.Pawn.StateHolder.SetState($"Has Best {slot.Type.DisplayName}", false);
                }
            }
            _npc.Pawn.StateHolder.SetState("Has Food", _npc.Pawn.Inventory.GetConsumable("Food", out _));
            _npc.Pawn.StateHolder.SetState("Has Drink", _npc.Pawn.Inventory.GetConsumable("Drink", out _));
            _npc.Pawn.StateHolder.SetState("Has Medical", _npc.Pawn.Inventory.GetConsumable("Medical", out _));
        }
    }
}