using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Heal Over Time", menuName = "Winter Universe/Effect/New Heal Over Time")]
    public class HealOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.HealOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new HealOverTimeEffect(this, target, source, value, duration);
        }
    }
}