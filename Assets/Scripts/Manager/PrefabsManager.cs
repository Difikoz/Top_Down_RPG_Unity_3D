using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pawnPrefab;

        public PawnController GetPawn(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnPrefab, position, rotation).GetComponent<PawnController>();
        }
    }
}