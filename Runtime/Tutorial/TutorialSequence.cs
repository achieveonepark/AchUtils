using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Tutorial
{
    [CreateAssetMenu(menuName = "AchUtils/Tutorial/Tutorial Sequence", fileName = "NewTutorialSequence")]
    public class TutorialSequence : ScriptableObject
    {
        public string SequenceId;
        public bool CanSkip = true;
        public bool SaveProgress = true;
        public List<TutorialStep> Steps = new();
    }
}
