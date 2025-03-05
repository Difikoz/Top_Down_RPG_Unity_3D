using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;

        private PawnController _pawn;
        private List<string> _stats = new();
        private float _healthCurrent;
        private float _healthMax;
        private float _healthRegeneration;
        private bool _isDead;

        [SerializeField] private float _regenerationCooldown = 0.2f;

        private float _regenerationTime;

        public List<string> Stats => _stats;
        public float HealthCurrent => _healthCurrent;
        public float HealthMax => _healthMax;
        public float HealthRegeneration => _healthRegeneration;
        public float HealthPercent => _healthCurrent / _healthMax;
        public bool IsDead => _isDead;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _healthCurrent = _healthMax;
        }

        public void OnUpdate()
        {
            if (_regenerationTime >= _regenerationCooldown)
            {
                RestoreHealthCurrent(_healthRegeneration / _regenerationCooldown);
                _regenerationTime = 0f;
            }
            else
            {
                _regenerationTime += Time.deltaTime;
            }
        }

        public void ReduceHealthCurrent(float value, string type, PawnController source = null)
        {
            if (_isDead || value <= 0f)
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            float resistance = 1f;// GetStat(type);
            if (resistance < 100f)
            {
                value -= (value * resistance / 100f);
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, _healthMax);
                if (_healthCurrent <= 0f)
                {
                    Die(source);
                }
                else
                {
                    OnHealthChanged?.Invoke(_healthCurrent, _healthMax);
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
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, _healthMax);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax);
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
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax);
            _isDead = true;
        }

        public void AddStatModifiers(List<string> modifiers)
        {
            foreach (string modifier in modifiers)
            {
                AddStatModifier(modifier);
            }
        }

        public void AddStatModifier(string modifier)
        {
            GetStat(modifier);//.AddModifier(modifier)
        }

        public void RemoveStatModifiers(List<string> modifiers)
        {
            foreach (string modifier in modifiers)
            {
                RemoveStatModifier(modifier);
            }
        }

        public void RemoveStatModifier(string modifier)
        {
            GetStat(modifier);//.RemoveModifier(modifier)
        }

        public string GetStat(string name)
        {
            foreach (string stat in _stats)
            {
                if (stat == name)
                {
                    return stat;
                }
            }
            return null;
        }
    }
}