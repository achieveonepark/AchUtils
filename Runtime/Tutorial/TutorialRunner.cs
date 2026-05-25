using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Tutorial
{
    public class TutorialRunner : MonoBehaviour
    {
        public TutorialSequence CurrentSequence { get; private set; }
        public int CurrentStepIndex { get; private set; }
        public bool IsRunning { get; private set; }

        private Coroutine _coroutine;
        private readonly HashSet<string> _triggeredActions = new();

        public event Action OnCompleted;
        public event Action OnSkipped;
        public event Action<int> OnStepChanged;

        public void Run(TutorialSequence sequence)
        {
            if (IsRunning) Stop();
            CurrentSequence = sequence;
            CurrentStepIndex = 0;
            _coroutine = StartCoroutine(Execute());
        }

        public void Skip()
        {
            if (!IsRunning || CurrentSequence == null || !CurrentSequence.CanSkip) return;
            Stop();
            OnSkipped?.Invoke();
        }

        public void Stop()
        {
            if (_coroutine != null) { StopCoroutine(_coroutine); _coroutine = null; }
            IsRunning = false;
            _triggeredActions.Clear();
        }

        public void TriggerAction(string actionKey) => _triggeredActions.Add(actionKey);

        public bool HasTriggeredAction(string actionKey) => _triggeredActions.Contains(actionKey);

        public void ConsumeAction(string actionKey) => _triggeredActions.Remove(actionKey);

        private IEnumerator Execute()
        {
            IsRunning = true;
            var steps = CurrentSequence.Steps;

            for (int i = 0; i < steps.Count; i++)
            {
                CurrentStepIndex = i;
                OnStepChanged?.Invoke(i);
                var step = steps[i];
                if (step != null)
                    yield return step.Execute(this);
            }

            IsRunning = false;
            _coroutine = null;

            OnCompleted?.Invoke();
        }
    }
}
