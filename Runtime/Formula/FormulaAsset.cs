using UnityEngine;

namespace AchieveOnePark.AchUtils.Formula
{
    [CreateAssetMenu(menuName = "SS/Formula/Formula Asset", fileName = "NewFormula")]
    public class FormulaAsset : ScriptableObject
    {
        [TextArea(3, 8)] public string Expression;

        private readonly FormulaEvaluator _evaluator = new();

        public float Evaluate(FormulaContext context)
        {
            return _evaluator.Evaluate(Expression, context);
        }
    }
}
