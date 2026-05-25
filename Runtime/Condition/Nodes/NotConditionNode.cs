using System;

namespace AchieveOnePark.AchUtils.Condition
{
    [Serializable]
    public class NotConditionNode : ConditionNode
    {
        [UnityEngine.SerializeReference] public ConditionNode Child;

        public override bool Evaluate(ConditionContext context)
        {
            return Child != null && !Child.Evaluate(context);
        }
    }
}
