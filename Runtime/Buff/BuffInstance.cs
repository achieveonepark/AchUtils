using UnityEngine;

namespace AchUtils.Buff
{
    public class BuffInstance
    {
        public BuffDefinition Definition { get; }
        public GameObject Target { get; }
        public int Stacks { get; set; }
        public float RemainingDuration { get; set; }
        public float TickTimer { get; set; }

        public bool IsPermanent => Definition.Duration < 0f;
        public bool IsExpired => !IsPermanent && RemainingDuration <= 0f;

        public BuffInstance(BuffDefinition definition, GameObject target)
        {
            Definition = definition;
            Target = target;
            Stacks = 1;
            RemainingDuration = definition.Duration;
            TickTimer = 0f;
        }

        public void ApplyEffects()
        {
            foreach (var effect in Definition.Effects)
                effect.OnApply(this, Target);
        }

        public void RemoveEffects()
        {
            foreach (var effect in Definition.Effects)
                effect.OnRemove(this, Target);
        }

        public void Tick()
        {
            foreach (var effect in Definition.Effects)
                effect.OnTick(this, Target);
        }
    }
}
