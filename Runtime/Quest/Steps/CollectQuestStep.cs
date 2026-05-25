using System;

namespace AchieveOnePark.AchUtils.Quest
{
    [Serializable]
    public class CollectQuestStep : QuestStep
    {
        public string ItemId;
        public int RequiredCount = 1;

        private int _current;

        public override bool IsComplete => _current >= RequiredCount;

        public override void OnProgress(string eventKey, object data)
        {
            if (eventKey != "Collect") return;
            if (data is string item && !string.IsNullOrEmpty(ItemId) && item != ItemId) return;
            _current++;
        }

        public override void OnStart() => _current = 0;
    }
}
