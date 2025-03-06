using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class PawnConfig : BasicInfoConfig
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private FactionConfig _faction;
        [SerializeField] private List<ItemStack> _stacks = new();

        public GameObject Model => _model;
        public FactionConfig Faction => _faction;
        public List<ItemStack> Stacks => _stacks;
    }
}