using System;

namespace AchieveOnePark.AchUtils.Quest
{
    [Serializable]
    public class TalkQuestStep : QuestStep
    {
        public string NpcId;
        private bool _done;

        public override bool IsComplete => _done;

        public override void OnProgress(string eventKey, object data)
        {
            if (eventKey != "Talk") return;
            if (data is string npc && !string.IsNullOrEmpty(NpcId) && npc != NpcId) return;
            _done = true;
        }

        public override void OnStart() => _done = false;
    }
}
