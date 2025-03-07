using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State Holder", menuName = "Winter Universe/ALIFE/New State Holder")]
    public class StateHolderConfig : ScriptableObject
    {
        [SerializeField] private List<StateCreator> _statesToChange = new();

        public List<StateCreator> StatesToChange => _statesToChange;
    }
}