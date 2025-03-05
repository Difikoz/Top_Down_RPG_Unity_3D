using UnityEngine;

namespace WinterUniverse
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage, string type, PawnController source = null);
    }
}