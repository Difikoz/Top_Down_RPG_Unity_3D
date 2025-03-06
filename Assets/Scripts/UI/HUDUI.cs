using UnityEngine;

namespace WinterUniverse
{
    public class HUDUI : MonoBehaviour
    {
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;
        [SerializeField] private VitalityBarUI _manaBar;

        private EffectsBarUI _effectsBar;

        public EffectsBarUI EffectsBar => _effectsBar;

        public void Initialize()
        {
            _effectsBar = GetComponentInChildren<EffectsBarUI>();
            _healthBar.Initialize();
            _energyBar.Initialize();
            _manaBar.Initialize();
            _effectsBar.Initialize();
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged += _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged += _energyBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnManaChanged += _manaBar.SetValues;
        }

        public void ResetComponent()
        {
            _effectsBar.ResetComponent();
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged -= _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged -= _energyBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnManaChanged -= _manaBar.SetValues;
        }
    }
}