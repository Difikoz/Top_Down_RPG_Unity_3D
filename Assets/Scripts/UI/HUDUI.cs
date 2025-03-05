using UnityEngine;

namespace WinterUniverse
{
    public class HUDUI : MonoBehaviour
    {
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;
        [SerializeField] private VitalityBarUI _manaBar;

        public void Initialize()
        {
            _healthBar.Initialize();
            _energyBar.Initialize();
            _manaBar.Initialize();
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged += _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged += _energyBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnManaChanged += _manaBar.SetValues;
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged -= _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged -= _energyBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnManaChanged -= _manaBar.SetValues;
        }
    }
}