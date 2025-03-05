using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class InventoryBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private WeaponSlot _weaponSlot;
        [SerializeField] private List<ArmorSlotUI> _armorSlots = new();

        public void Initialize()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Inventory.OnInventoryChanged += OnInventoryChanged;
            OnInventoryChanged();
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Inventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged()
        {
            while (_contentRoot.childCount > 0)
            {
                LeanPool.Despawn(_contentRoot.GetChild(0).gameObject);
            }
            foreach (ItemConfig item in GameManager.StaticInstance.PlayerManager.Pawn.Inventory.Items)
            {
                ShowFullInformation(item);
                LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<InventorySlotUI>().Initialize(item);
            }
        }

        public void OnEquipmentChanged()
        {

        }

        public void ShowFullInformation(ItemConfig config)
        {
            _nameText.text = config.DisplayName;
            _descriptionText.text = config.Description;
        }
    }
}