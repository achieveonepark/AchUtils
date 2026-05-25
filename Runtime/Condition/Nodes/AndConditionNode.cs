using System;
using System.Collections.Generic;
using System.Linq;

namespace AchieveOnePark.AchUtils.Condition
{
    [Serializable]
    public class AndConditionNode : ConditionNode
    {
        [UnityEngine.SerializeReference] public List<ConditionNode> Children = new();

        public override bool Evaluate(ConditionContext context)
        {
            return Children.All(c => c.Evaluate(context));
        }
    }
}
