using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Sequence
{
    [Serializable]
    public class ScaleStep : SequenceStep
    {
        public Transform Target;
        public Vector3 TargetScale = Vector3.one;
        public float Duration = 0.3f;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override IEnumerator Execute(SequenceRunner runner)
        {
            if (Target == null) yield break;

            Vector3 startScale = Target.localScale;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                Target.localScale = Vector3.Lerp(startScale, TargetScale, Ease.Evaluate(Mathf.Clamp01(elapsed / Duration)));
                yield return null;
            }

            Target.localScale = TargetScale;
        }
    }
}
