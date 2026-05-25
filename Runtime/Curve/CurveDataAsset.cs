using UnityEngine;

namespace AchUtils.Curve
{
    [CreateAssetMenu(menuName = "AchUtils/Curve/Curve Data Asset", fileName = "NewCurveData")]
    public class CurveDataAsset : ScriptableObject
    {
        public AnimationCurve Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public float MinInput = 0f;
        public float MaxInput = 100f;
        public float MinOutput = 0f;
        public float MaxOutput = 1000f;

        public float Evaluate(float input)
        {
            float t = Mathf.InverseLerp(MinInput, MaxInput, input);
            return Mathf.Lerp(MinOutput, MaxOutput, Curve.Evaluate(t));
        }

        public float EvaluateNormalized(float t)
        {
            return Mathf.Lerp(MinOutput, MaxOutput, Curve.Evaluate(Mathf.Clamp01(t)));
        }
    }
}
