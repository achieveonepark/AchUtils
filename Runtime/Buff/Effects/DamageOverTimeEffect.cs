using System;
using UnityEngine;

namespace AchUtils.Buff
{
    public interface IDamageable
    {
        void TakeDamage(float amount);
    }

    [Serializable]
    public class DamageOverTimeEffect : BuffEffect
    {
        public float DamagePerTick = 10f;

        public override void OnTick(BuffInstance buff, GameObject target)
        {
            var damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(DamagePerTick * buff.Stacks);
        }
    }
}
