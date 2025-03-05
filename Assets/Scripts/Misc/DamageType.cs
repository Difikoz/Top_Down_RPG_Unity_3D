using UnityEngine;

namespace WinterUniverse
{
    public class DamageType
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private string _type;

        public float Damage => _damage;
        public string Type => _type;
    }
}