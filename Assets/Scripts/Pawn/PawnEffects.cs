using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEffects : MonoBehaviour
    {
        private PawnController _pawn;
        private List<Effect> _allEffects = new();

        [SerializeField] private float _tickCooldown = 0.5f;

        private float _tickTime;

        public List<Effect> AllEffects => _allEffects;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _allEffects.Clear();
        }

        public void OnUpdate()
        {
            if (_tickTime >= _tickCooldown)
            {
                HandleEffects();
                _tickTime = 0f;
            }
            else
            {
                _tickTime += Time.deltaTime;
            }
        }

        public void AddEffect(Effect effect)
        {
            _allEffects.Add(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            if (_allEffects.Contains(effect))
            {
                _allEffects.Remove(effect);
            }
        }

        private void HandleEffects()
        {
            foreach (Effect effect in _allEffects)
            {
                effect.OnTick(_tickCooldown);
            }
        }
    }
}