using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Buff
{
    [Serializable]
    public abstract class BuffEffect
    {
        public virtual void OnApply(BuffInstance buff, GameObject target) { }
        public virtual void OnRemove(BuffInstance buff, GameObject target) { }
        public virtual void OnTick(BuffInstance buff, GameObject target) { }
        public virtual void OnStackAdded(BuffInstance buff, GameObject target, int newStack) { }
    }
}
