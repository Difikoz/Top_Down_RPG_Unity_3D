using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private bool _testInteract;
        [SerializeField] private PawnController _pawnToInteract;

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