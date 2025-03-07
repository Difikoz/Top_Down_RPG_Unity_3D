using UnityEngine;

namespace WinterUniverse
{
    public class ActionUseConsumable : ActionBase
    {
        [SerializeField] private ConsumableTypeConfig _consumableType;
        private ConsumableItemConfig _consumable;

        public override bool CanStart()
        {
            if (_npc.Pawn.Inventory.GetConsumable(_consumableType.DisplayName, out _consumable))
            {
                return base.CanStart();
            }
            return false;
        }

        public override void OnStart()
        {
            base.OnStart();
            _consumable.Use(_npc.Pawn);
            _consumable = null;
        }
    }
}