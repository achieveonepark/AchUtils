using System.Collections.Generic;

namespace AchUtils.Condition
{
    public class ConditionContext
    {
        private readonly Dictionary<string, float> _values = new();

        public void Set(string key, float value) => _values[key] = value;

        public float Get(string key) => _values.TryGetValue(key, out var v) ? v : 0f;

        public bool Has(string key) => _values.ContainsKey(key);

        public void Remove(string key) => _values.Remove(key);

        public void Clear() => _values.Clear();
    }
}
