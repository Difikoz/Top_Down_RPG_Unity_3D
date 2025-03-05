using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ConfigsManager : MonoBehaviour
    {
        [SerializeField] private List<WeaponItemConfig> _weapons = new();
        [SerializeField] private List<ArmorItemConfig> _armors = new();
        [SerializeField] private List<ElementConfig> _elements = new();
        [SerializeField] private List<StatConfig> _stats = new();

        private List<ItemConfig> _items = new();

        public List<ItemConfig> Items => _items;
        public List<ElementConfig> Elements => _elements;
        public List<StatConfig> Stats => _stats;

        public void Initialize()
        {
            _items.Clear();
            foreach (WeaponItemConfig config in _weapons)
            {
                _items.Add(config);
            }
            foreach (ArmorItemConfig config in _armors)
            {
                _items.Add(config);
            }
        }

        public ItemConfig GetItem(string name)
        {
            foreach (ItemConfig item in _items)
            {
                if (item.DisplayName == name)
                {
                    return item;
                }
            }
            return null;
        }

        public WeaponItemConfig GetWeapon(string name)
        {
            foreach (WeaponItemConfig item in _weapons)
            {
                if (item.DisplayName == name)
                {
                    return item;
                }
            }
            return null;
        }

        public ArmorItemConfig GetArmor(string name)
        {
            foreach (ArmorItemConfig item in _armors)
            {
                if (item.DisplayName == name)
                {
                    return item;
                }
            }
            return null;
        }

        public ElementConfig GetElement(string name)
        {
            foreach (ElementConfig element in _elements)
            {
                if (element.DisplayName == name)
                {
                    return element;
                }
            }
            return null;
        }

        public StatConfig GetStat(string name)
        {
            foreach (StatConfig stat in _stats)
            {
                if (stat.DisplayName == name)
                {
                    return stat;
                }
            }
            return null;
        }
    }
}