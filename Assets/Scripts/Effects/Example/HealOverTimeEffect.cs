using UnityEngine;

namespace WinterUniverse
{
    public class HealOverTimeEffect : Effect
    {
        public HealOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.RestoreHealthCurrent(_value * deltaTime);
        }
    }
}