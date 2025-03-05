using UnityEngine;

namespace WinterUniverse
{
    public class DamageType
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private ElementConfig _type;

        public float Damage => _damage;
        public ElementConfig Type => _type;
    }
}