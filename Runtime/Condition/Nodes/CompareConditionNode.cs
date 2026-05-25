using System;

namespace AchUtils.Condition
{
    public enum CompareOp { Equal, NotEqual, Greater, GreaterOrEqual, Less, LessOrEqual }

    [Serializable]
    public class CompareConditionNode : ConditionNode
    {
        public string Key;
        public CompareOp Op;
        public float Value;

        public override bool Evaluate(ConditionContext context)
        {
            float v = context.Get(Key);
            return Op switch
            {
                CompareOp.Equal => MathF.Abs(v - Value) < 0.0001f,
                CompareOp.NotEqual => MathF.Abs(v - Value) >= 0.0001f,
                CompareOp.Greater => v > Value,
                CompareOp.GreaterOrEqual => v >= Value,
                CompareOp.Less => v < Value,
                CompareOp.LessOrEqual => v <= Value,
                _ => false
            };
        }
    }
}
