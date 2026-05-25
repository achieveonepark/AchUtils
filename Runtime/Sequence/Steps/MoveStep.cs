using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    [Serializable]
    public class MoveStep : SequenceStep
    {
        public Transform Target;
        public Vector3 Destination;
        public float Duration = 1f;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        public bool UseLocalSpace = false;

        public override IEnumerator Execute(SequenceRunner runner)
        {
            if (Target == null) yield break;

            Vector3 start = UseLocalSpace ? Target.localPosition : Target.position;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                float t = Ease.Evaluate(Mathf.Clamp01(elapsed / Duration));
                Vector3 pos = Vector3.Lerp(start, Destination, t);
                if (UseLocalSpace) Target.localPosition = pos;
                else Target.position = pos;
                yield return null;
            }

            if (UseLocalSpace) Target.localPosition = Destination;
            else Target.position = Destination;
        }
    }
}
