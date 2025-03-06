using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Mana Over Time", menuName = "Winter Universe/Effect/New Restore Mana Over Time")]
    public class RestoreManaOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.RestoreManaOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new RestoreManaOverTimeEffect(this, target, source, value, duration);
        }
    }
}