using UnityEngine;

namespace AchieveOnePark.AchUtils.Condition
{
    [CreateAssetMenu(menuName = "SS/Condition/Condition Graph", fileName = "NewConditionGraph")]
    public class ConditionGraph : ScriptableObject
    {
        [SerializeReference] public ConditionNode Root;

        public bool Evaluate(ConditionContext context)
        {
            return Root?.Evaluate(context) ?? false;
        }
    }
}
