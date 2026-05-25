using System;

namespace AchUtils.StatModifier
{
    public enum ModifierType
    {
        Flat,
        PercentAdd,
        PercentMultiply
    }

    [Serializable]
    public struct StatModifier
    {
        public string Source;
        public ModifierType Type;
        public float Value;
        public int Priority;

        public StatModifier(string source, ModifierType type, float value, int priority = 0)
        {
            Source = source;
            Type = type;
            Value = value;
            Priority = priority;
        }
    }
}
