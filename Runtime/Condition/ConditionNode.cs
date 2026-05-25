using System;
using UnityEngine;

namespace AchUtils.Condition
{
    [Serializable]
    public abstract class ConditionNode
    {
        public abstract bool Evaluate(ConditionContext context);
    }
}
