using System;

namespace AchUtils.Quest
{
    public class QuestInstance
    {
        public QuestDefinition Definition { get; }
        public int CurrentStepIndex { get; private set; }
        public bool IsCompleted { get; private set; }

        public event Action<int> OnStepCompleted;
        public event Action OnQuestCompleted;

        public QuestStep CurrentStep =>
            CurrentStepIndex < Definition.Steps.Count
                ? Definition.Steps[CurrentStepIndex]
                : null;

        public QuestInstance(QuestDefinition definition)
        {
            Definition = definition;
            CurrentStep?.OnStart();
        }

        public void Progress(string eventKey, object data)
        {
            if (IsCompleted || CurrentStep == null) return;

            CurrentStep.OnProgress(eventKey, data);

            if (CurrentStep.IsComplete)
            {
                CurrentStep.OnComplete();
                OnStepCompleted?.Invoke(CurrentStepIndex);
                CurrentStepIndex++;

                if (CurrentStepIndex >= Definition.Steps.Count)
                {
                    IsCompleted = true;
                    OnQuestCompleted?.Invoke();
                }
                else
                {
                    CurrentStep?.OnStart();
                }
            }
        }
    }
}
