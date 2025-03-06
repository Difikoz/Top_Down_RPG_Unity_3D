using UnityEngine;

namespace WinterUniverse
{
    public class ReduceManaOverTimeEffect : Effect
    {
        public ReduceManaOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.ReduceManaCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.ReduceManaCurrent(_value * deltaTime);
        }
    }
}