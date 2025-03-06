using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private GameObject _itemPrefab;

        public PawnController GetPawn(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnPrefab, position, rotation).GetComponent<PawnController>();
        }

        public InteractableItem GetItem(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_itemPrefab, position, rotation).GetComponent<InteractableItem>();
        }
    }
}