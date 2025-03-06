using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Reduce Mana Over Time", menuName = "Winter Universe/Effect/New Reduce Mana Over Time")]
    public class ReduceManaOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.ReduceManaOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new ReduceManaOverTimeEffect(this, target, source, value, duration);
        }
    }
}