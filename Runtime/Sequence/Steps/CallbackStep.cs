using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AchieveOnePark.AchUtils.Sequence
{
    [Serializable]
    public class CallbackStep : SequenceStep
    {
        public UnityEvent OnExecute;

        public override IEnumerator Execute(SequenceRunner runner)
        {
            OnExecute?.Invoke();
            yield return null;
        }
    }
}
