using UnityEngine;

namespace WinterUniverse
{
    public class RestoreManaOverTimeEffect : Effect
    {
        public RestoreManaOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.RestoreManaCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.RestoreManaCurrent(_value * deltaTime);
        }
    }
}