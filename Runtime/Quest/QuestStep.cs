using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Quest
{
    [Serializable]
    public abstract class QuestStep
    {
        public string Description;

        public abstract bool IsComplete { get; }
        public abstract void OnProgress(string eventKey, object data);
        public virtual void OnStart() { }
        public virtual void OnComplete() { }
    }
}
