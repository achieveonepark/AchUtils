using System.Collections;
using UnityEngine;

namespace AchUtils.Tutorial
{
    [CreateAssetMenu(menuName = "AchUtils/Tutorial/Steps/Delay", fileName = "DelayStep")]
    public class DelayStep : TutorialStep
    {
        public float Duration = 1f;

        public override IEnumerator Execute(TutorialRunner runner)
        {
            yield return new WaitForSeconds(Duration);
        }
    }
}
