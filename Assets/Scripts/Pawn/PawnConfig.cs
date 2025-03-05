using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class PawnConfig : BasicInfoConfig
    {
        [SerializeField] private GameObject _model;

        public GameObject Model => _model;
    }
}