using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private ItemConfig _testItem;

        private ItemConfig _item;
        private int _amount = 1;
        private GameObject _model;

        private void Awake()
        {
            Initialize(_testItem);
        }

        public void Initialize(ItemConfig item, int amount = 1)
        {
            _item = item;
            _amount = amount;
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
            _model = LeanPool.Spawn(_item.Model, transform);
        }

        public override bool CanInteract(PawnController pawn)
        {
            return _item != null && _amount > 0 && !pawn.Status.IsDead && !pawn.Animator.IsPerfomingAction;
        }

        public override void Interact(PawnController pawn)
        {
            pawn.Inventory.AddItem(_item);
            LeanPool.Despawn(gameObject);
        }
    }
}