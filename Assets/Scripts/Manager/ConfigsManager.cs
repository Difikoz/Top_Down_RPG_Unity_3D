using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ConfigsManager : MonoBehaviour
    {
        [SerializeField] private List<WeaponItemConfig> _weapons = new();
        [SerializeField] private List<ArmorItemConfig> _armors = new();
        [SerializeField] private List<ConsumableItemConfig> _consumables = new();
        [SerializeField] private List<ResourceItemConfig> _resources = new();
        [SerializeField] private List<ElementConfig> _elements = new();
        [SerializeField] private List<StatConfig> _stats = new();
        [SerializeField] private List<FactionConfig> _factions = new();
        [SerializeField] private List<StateConfig> _pawnStates = new();
        [SerializeField] private List<StateConfig> _worldStates = new();

        private List<ItemConfig> _items = new();

        public List<ItemConfig> Items => _items;
        public List<ElementConfig> Elements => _elements;
        public List<StatConfig> Stats => _stats;
        public List<FactionConfig> Factions => _factions;
        public List<StateConfig> PawnStates => _pawnStates;
        public List<StateConfig> WorldStates => _worldStates;

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
            foreach (ConsumableItemConfig config in _consumables)
            {
                _items.Add(config);
            }
            foreach (ResourceItemConfig config in _resources)
            {
                _items.Add(config);
            }
        }

        public ItemConfig GetItem(string name)
        {
            foreach (ItemConfig config in _items)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public WeaponItemConfig GetWeapon(string name)
        {
            foreach (WeaponItemConfig config in _weapons)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public ArmorItemConfig GetArmor(string name)
        {
            foreach (ArmorItemConfig config in _armors)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public ConsumableItemConfig GetConsumable(string name)
        {
            foreach (ConsumableItemConfig config in _consumables)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public ResourceItemConfig GetResource(string name)
        {
            foreach (ResourceItemConfig config in _resources)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public ElementConfig GetElement(string name)
        {
            foreach (ElementConfig config in _elements)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public StatConfig GetStat(string name)
        {
            foreach (StatConfig config in _stats)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }

        public FactionConfig GetFaction(string name)
        {
            foreach (FactionConfig config in _factions)
            {
                if (config.DisplayName == name)
                {
                    return config;
                }
            }
            return null;
        }
    }
}