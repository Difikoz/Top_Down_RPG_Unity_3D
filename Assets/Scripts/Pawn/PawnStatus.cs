using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action OnStatsChanged;

        private PawnController _pawn;
        private bool _isDead;

        private List<Stat> _stats = new();

        private float _healthCurrent;
        private Stat _healthMax;
        private Stat _healthRegeneration;
        private Stat _acceleration;
        private Stat _moveSpeed;
        private Stat _rotateSpeed;
        private Stat _attackSpeed;
        private Stat _damageDealt;
        private Stat _slicingDamage;
        private Stat _piercingDamage;
        private Stat _bluntDamage;
        private Stat _damageTaken;
        private Stat _slicingResistance;
        private Stat _piercingResistance;
        private Stat _bluntResistance;

        [SerializeField] private float _regenerationCooldown = 0.2f;

        private float _regenerationTime;

        public List<Stat> Stats => _stats;
        public float HealthCurrent => _healthCurrent;
        public Stat HealthMax => _healthMax;
        public Stat HealthRegeneration => _healthRegeneration;
        public Stat Acceleration => _acceleration;
        public Stat MoveSpeed => _moveSpeed;
        public Stat RotateSpeed => _rotateSpeed;
        public Stat AttackSpeed => _attackSpeed;
        public Stat DamageDealt => _damageDealt;
        public Stat SlicingDamage => _slicingDamage;
        public Stat PiercingDamage => _piercingDamage;
        public Stat BluntDamage => _bluntDamage;
        public Stat DamageTaken => _damageTaken;
        public Stat SlicingResistance => _slicingResistance;
        public Stat PiercingResistance => _piercingResistance;
        public Stat BluntResistance => _bluntResistance;
        public float HealthPercent => _healthCurrent / _healthMax.CurrentValue;
        public bool IsDead => _isDead;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            CreateStats();
            AssignStats();
            RecalculateStats();
        }

        public void ResetComponent()
        {

        }

        private void CreateStats()
        {
            foreach (StatConfig stat in GameManager.StaticInstance.ConfigsManager.Stats)
            {
                _stats.Add(new(stat));
            }
        }

        private void AssignStats()
        {
            foreach (Stat s in _stats)
            {
                switch (s.Config.DisplayName)
                {
                    case "Health Max":
                        _healthMax = s;
                        break;
                    case "Health Regeneration":
                        _healthRegeneration = s;
                        break;
                    case "Acceleration":
                        _acceleration = s;
                        break;
                    case "Move Speed":
                        _moveSpeed = s;
                        break;
                    case "Rotate Speed":
                        _rotateSpeed = s;
                        break;
                    case "Attack Speed":
                        _attackSpeed = s;
                        break;
                    case "Slicing Damage":
                        _slicingDamage = s;
                        break;
                    case "Damage Dealt":
                        _damageDealt = s;
                        break;
                    case "Piercing Damage":
                        _piercingDamage = s;
                        break;
                    case "Blunt Damage":
                        _bluntDamage = s;
                        break;
                    case "Damage Taken":
                        _damageTaken = s;
                        break;
                    case "Slicing Resistance":
                        _slicingResistance = s;
                        break;
                    case "Piercing Resistance":
                        _piercingResistance = s;
                        break;
                    case "Blunt Resistance":
                        _bluntResistance = s;
                        break;
                }
            }
        }

        public void RecalculateStats()
        {
            _healthCurrent = Mathf.Clamp(_healthCurrent, 0f, _healthMax.CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            OnStatsChanged?.Invoke();
        }

        public void OnUpdate()
        {
            if (_regenerationTime >= _regenerationCooldown)
            {
                RestoreHealthCurrent(_healthRegeneration.CurrentValue / _regenerationCooldown);
                _regenerationTime = 0f;
            }
            else
            {
                _regenerationTime += Time.deltaTime;
            }
        }

        public void ReduceHealthCurrent(float value, ElementConfig type, PawnController source = null)
        {
            if (_isDead || value <= 0f)
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            float resistance = GetStat(type.ResistanceStat.DisplayName).CurrentValue;
            if (resistance < 100f)
            {
                value -= (value * resistance / 100f);
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, _healthMax.CurrentValue);
                if (_healthCurrent <= 0f)
                {
                    Die(source);
                }
                else
                {
                    OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
                }
            }
            else if (resistance > 100f)
            {
                value *= (resistance / 100f - 1f);
                RestoreHealthCurrent(value);
            }
        }

        public void RestoreHealthCurrent(float value)
        {
            if (_isDead || value <= 0f)
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, _healthMax.CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
        }

        private void Die(PawnController source = null)
        {
            if (_isDead)
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            _healthCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            _isDead = true;
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        public void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.DisplayName).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.DisplayName).RemoveModifier(smc.Modifier);
        }

        public Stat GetStat(string name)
        {
            foreach (Stat stat in _stats)
            {
                if (stat.Config.DisplayName == name)
                {
                    return stat;
                }
            }
            return null;
        }
    }
}