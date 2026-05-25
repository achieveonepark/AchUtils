using System;
using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.Quest
{
    public class QuestSystem
    {
        private readonly List<QuestInstance> _activeQuests = new();
        private readonly HashSet<string> _completedQuestIds = new();

        public event Action<QuestInstance> OnQuestStarted;
        public event Action<QuestInstance> OnQuestCompleted;
        public event Action<QuestInstance, int> OnQuestStepCompleted;

        public QuestInstance StartQuest(QuestDefinition definition)
        {
            if (definition == null) return null;
            if (_completedQuestIds.Contains(definition.QuestId)) return null;
            if (_activeQuests.Exists(q => q.Definition.QuestId == definition.QuestId)) return null;

            var instance = new QuestInstance(definition);
            instance.OnQuestCompleted += () =>
            {
                _completedQuestIds.Add(definition.QuestId);
                _activeQuests.Remove(instance);
                OnQuestCompleted?.Invoke(instance);
            };
            instance.OnStepCompleted += step => OnQuestStepCompleted?.Invoke(instance, step);

            _activeQuests.Add(instance);
            OnQuestStarted?.Invoke(instance);
            return instance;
        }

        public void Progress(string eventKey, object data = null)
        {
            foreach (var quest in _activeQuests)
                quest.Progress(eventKey, data);
        }

        public bool IsCompleted(string questId) => _completedQuestIds.Contains(questId);

        public bool IsActive(string questId) =>
            _activeQuests.Exists(q => q.Definition.QuestId == questId);

        public QuestInstance GetActive(string questId) =>
            _activeQuests.Find(q => q.Definition.QuestId == questId);

        public List<QuestInstance> GetAllActive() => new(_activeQuests);
    }
}
