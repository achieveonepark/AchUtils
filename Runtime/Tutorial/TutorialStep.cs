using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Tutorial
{
    public abstract class TutorialStep : ScriptableObject
    {
        public string StepId;
        [TextArea] public string Description;

        public abstract IEnumerator Execute(TutorialRunner runner);

        public virtual void OnSkip(TutorialRunner runner) { }
    }
}
