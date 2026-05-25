using System;
using System.Collections.Generic;
using System.Linq;

namespace AchieveOnePark.AchUtils.Condition
{
    [Serializable]
    public class OrConditionNode : ConditionNode
    {
        [UnityEngine.SerializeReference] public List<ConditionNode> Children = new();

        public override bool Evaluate(ConditionContext context)
        {
            return Children.Any(c => c.Evaluate(context));
        }
    }
}
