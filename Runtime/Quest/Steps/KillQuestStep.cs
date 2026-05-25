using System;

namespace AchieveOnePark.AchUtils.Quest
{
    [Serializable]
    public class KillQuestStep : QuestStep
    {
        public string EnemyType;
        public int RequiredCount = 5;

        private int _current;

        public override bool IsComplete => _current >= RequiredCount;

        public override void OnProgress(string eventKey, object data)
        {
            if (eventKey != "Kill") return;
            if (data is string type && !string.IsNullOrEmpty(EnemyType) && type != EnemyType) return;
            _current++;
        }

        public override void OnStart() => _current = 0;
    }
}
