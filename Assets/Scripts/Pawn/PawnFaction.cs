using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnFaction : MonoBehaviour
    {
        public Action OnFactionChanged;

        private PawnController _pawn;
        private FactionConfig _config;

        public FactionConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            Change(_pawn.Config.Faction);
        }

        public void Change(FactionConfig config)
        {
            _config = config;
            OnFactionChanged?.Invoke();
        }
    }
}