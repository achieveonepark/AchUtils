using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    [Serializable]
    public class FadeStep : SequenceStep
    {
        public CanvasGroup Target;
        [Range(0f, 1f)] public float TargetAlpha = 0f;
        public float Duration = 0.5f;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override IEnumerator Execute(SequenceRunner runner)
        {
            if (Target == null) yield break;

            float startAlpha = Target.alpha;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                Target.alpha = Mathf.Lerp(startAlpha, TargetAlpha, Ease.Evaluate(Mathf.Clamp01(elapsed / Duration)));
                yield return null;
            }

            Target.alpha = TargetAlpha;
        }
    }
}
