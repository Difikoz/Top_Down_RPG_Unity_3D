using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private Transform _pointToInteract;
        [SerializeField] private float _distanceToInteract = 1f;

        public Transform PointToInteract => _pointToInteract != null ? _pointToInteract : transform;
        public float DistanceToInteract => _distanceToInteract;

        public abstract bool CanInteract(PawnController pawn);
        public abstract void Interact(PawnController pawn);
    }
}