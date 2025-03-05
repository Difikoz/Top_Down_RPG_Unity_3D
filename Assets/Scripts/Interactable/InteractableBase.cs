using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private bool _testInteract;
        [SerializeField] private PawnController _pawnToInteract;
        [SerializeField] private Transform _pointToInteract;
        [SerializeField] private float _distanceToInteract = 1f;

        public Transform PointToInteract => _pointToInteract != null ? _pointToInteract : transform;
        public float DistanceToInteract => _distanceToInteract;

        protected virtual void Update()
        {
            if (_testInteract)
            {
                _testInteract = false;
                if (CanInteract(_pawnToInteract))
                {
                    Interact(_pawnToInteract);
                }
            }
        }

        public abstract bool CanInteract(PawnController pawn);
        public abstract void Interact(PawnController pawn);
    }
}