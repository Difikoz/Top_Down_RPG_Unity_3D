using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage Over Time", menuName = "Winter Universe/Effect/New Damage Over Time")]
    public class DamageOverTimeEffectConfig : EffectConfig
    {
        [SerializeField] private ElementConfig _element;

        private void OnValidate()
        {
            _effectType = EffectType.DamageOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new DamageOverTimeEffect(this, target, source, value, duration, _element);
        }
    }
}