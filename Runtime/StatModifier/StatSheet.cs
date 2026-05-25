using System;
using System.Collections.Generic;

namespace AchUtils.StatModifier
{
    public class StatSheet
    {
        private readonly Dictionary<string, StatValue> _stats = new();

        public event Action<string> OnStatChanged;

        public StatValue GetStat(string key)
        {
            if (!_stats.TryGetValue(key, out var stat))
            {
                stat = new StatValue();
                stat.OnChanged += () => OnStatChanged?.Invoke(key);
                _stats[key] = stat;
            }
            return stat;
        }

        public float GetFinal(string key) => GetStat(key).FinalValue;

        public void SetBase(string key, float value) => GetStat(key).BaseValue = value;

        public void AddModifier(string key, StatModifier modifier) => GetStat(key).AddModifier(modifier);

        public void RemoveModifier(string key, string source) => GetStat(key).RemoveModifier(source);

        public void RemoveAllModifiers(string key) => GetStat(key).RemoveAllModifiers();

        public bool HasStat(string key) => _stats.ContainsKey(key);
    }
}
