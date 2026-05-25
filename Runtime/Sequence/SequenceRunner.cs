using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    public class SequenceRunner : MonoBehaviour
    {
        private Coroutine _current;

        public bool IsRunning { get; private set; }

        public event Action OnCompleted;
        public event Action OnStopped;

        public void Run(SequenceAsset asset)
        {
            if (asset != null) Run(asset.Steps);
        }

        public void Run(IEnumerable<SequenceStep> steps)
        {
            Stop();
            _current = StartCoroutine(Execute(steps));
        }

        public void Stop()
        {
            if (_current != null)
            {
                StopCoroutine(_current);
                _current = null;
                IsRunning = false;
                OnStopped?.Invoke();
            }
        }

        private IEnumerator Execute(IEnumerable<SequenceStep> steps)
        {
            IsRunning = true;
            foreach (var step in steps)
            {
                if (step == null) continue;
                yield return step.Execute(this);
            }
            IsRunning = false;
            _current = null;
            OnCompleted?.Invoke();
        }
    }
}
