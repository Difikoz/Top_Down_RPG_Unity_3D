using UnityEngine;

namespace WinterUniverse
{
    public class ActionCombat : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy(), true, true);
        }
    }
}