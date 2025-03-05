using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;
        private WeaponSlot _weaponSlot;
        private List<ArmorSlot> _armorSlots = new();

        public WeaponSlot WeaponSlot => _weaponSlot;
        public List<ArmorSlot> ArmorSlots => _armorSlots;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _weaponSlot = GetComponentInChildren<WeaponSlot>();
            ArmorSlot[] armorSlots = GetComponentsInChildren<ArmorSlot>();
            foreach (ArmorSlot slot in armorSlots)
            {
                _armorSlots.Add(slot);
            }
        }

        public void ResetComponent()
        {
            _armorSlots.Clear();
        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            if (addOldToInventory && _weaponSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.ChangeConfig(config);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (addOldToInventory && _weaponSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.ChangeConfig(null);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == config.ArmorType)
                {
                    if (removeNewFromInventory)
                    {
                        _pawn.Inventory.RemoveItem(config);
                    }
                    if (addOldToInventory && slot.Config != null)
                    {
                        _pawn.Inventory.AddItem(slot.Config);
                    }
                    slot.ChangeConfig(config);
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(int index, bool addOldToInventory = true)
        {
            if (index >= _armorSlots.Count)
            {
                return;
            }
            UnequipArmor(_armorSlots[index], addOldToInventory);
        }

        public void UnequipArmor(string type, bool addOldToInventory = true)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type.DisplayName == type)
                {
                    UnequipArmor(slot, addOldToInventory);
                    break;
                }
            }
        }

        public void UnequipArmor(ArmorSlot slot, bool addOldToInventory = true)
        {
            if (addOldToInventory && slot.Config != null)
            {
                _pawn.Inventory.AddItem(slot.Config);
            }
            slot.ChangeConfig(null);
            OnEquipmentChanged?.Invoke();
        }
    }
}