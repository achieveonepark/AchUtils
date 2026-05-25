using System;
using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.StatModifier
{
    public class StatValue
    {
        private float _baseValue;
        private readonly List<StatModifier> _modifiers = new();

        public event Action OnChanged;

        public StatValue(float baseValue = 0f)
        {
            _baseValue = baseValue;
        }

        public float BaseValue
        {
            get => _baseValue;
            set { _baseValue = value; OnChanged?.Invoke(); }
        }

        public float FinalValue
        {
            get
            {
                float flat = _baseValue;
                float percentAdd = 0f;
                float percentMult = 1f;

                foreach (var mod in _modifiers)
                {
                    switch (mod.Type)
                    {
                        case ModifierType.Flat: flat += mod.Value; break;
                        case ModifierType.PercentAdd: percentAdd += mod.Value; break;
                        case ModifierType.PercentMultiply: percentMult *= 1f + mod.Value; break;
                    }
                }

                return flat * (1f + percentAdd) * percentMult;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            OnChanged?.Invoke();
        }

        public bool RemoveModifier(string source)
        {
            int removed = _modifiers.RemoveAll(m => m.Source == source);
            if (removed > 0) OnChanged?.Invoke();
            return removed > 0;
        }

        public void RemoveAllModifiers()
        {
            _modifiers.Clear();
            OnChanged?.Invoke();
        }
    }
}
