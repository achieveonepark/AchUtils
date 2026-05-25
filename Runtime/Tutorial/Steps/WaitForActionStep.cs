using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Tutorial
{
    [CreateAssetMenu(menuName = "AchUtils/Tutorial/Steps/Wait For Action", fileName = "WaitForActionStep")]
    public class WaitForActionStep : TutorialStep
    {
        public string ActionKey;
        public float Timeout = 0f;

        public override IEnumerator Execute(TutorialRunner runner)
        {
            if (string.IsNullOrEmpty(ActionKey)) yield break;

            float elapsed = 0f;

            while (!runner.HasTriggeredAction(ActionKey))
            {
                if (Timeout > 0f)
                {
                    elapsed += Time.deltaTime;
                    if (elapsed >= Timeout) break;
                }
                yield return null;
            }

            runner.ConsumeAction(ActionKey);
        }
    }
}
