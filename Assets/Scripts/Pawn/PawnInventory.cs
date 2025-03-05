using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        private PawnController _pawn;
        private List<ItemConfig> _items = new();

        public List<ItemConfig> Items => _items;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _items.Clear();
        }

        public void AddItem(ItemConfig config, int amount = 1)
        {
            while (amount > 0)
            {
                _items.Add(config);
                amount--;
            }
            //OnInventoryChanged?.Invoke();
        }

        public void RemoveItem(ItemConfig config, int amount = 1)
        {
            if (AmountOfItem(config) < amount)
            {
                Debug.LogError($"Not Enough {amount} {config.DisplayName} in {_pawn.gameObject.name} Inventory (Has {AmountOfItem(config)}).");
                return;
            }
            while (amount > 0)
            {
                _items.Remove(config);
                amount--;
            }
            //OnInventoryChanged?.Invoke();
        }

        public int AmountOfItem(ItemConfig config)
        {
            int amount = 0;
            foreach (ItemConfig item in _items)
            {
                if (item == config)
                {
                    amount++;
                }
            }
            return amount;
        }
    }
}