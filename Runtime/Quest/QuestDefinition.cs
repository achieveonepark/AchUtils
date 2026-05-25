using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Quest
{
    [CreateAssetMenu(menuName = "AchUtils/Quest/Quest Definition", fileName = "NewQuest")]
    public class QuestDefinition : ScriptableObject
    {
        public string QuestId;
        public string Title;
        [TextArea(2, 6)] public string Description;
        public Sprite Icon;

        [SerializeReference] public List<QuestStep> Steps = new();
        public List<QuestReward> Rewards = new();
    }
}
