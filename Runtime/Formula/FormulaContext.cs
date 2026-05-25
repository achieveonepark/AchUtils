using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.Formula
{
    public class FormulaContext
    {
        private readonly Dictionary<string, float> _variables = new();

        public void Set(string key, float value) => _variables[key] = value;

        public float Get(string key)
        {
            if (_variables.TryGetValue(key, out var v)) return v;
            throw new System.Exception($"Variable '{key}' not found in FormulaContext");
        }

        public bool Has(string key) => _variables.ContainsKey(key);

        public void Clear() => _variables.Clear();
    }
}
