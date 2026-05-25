using System;
using UnityEngine;

namespace AchUtils.Buff
{
    public interface IStunnable
    {
        void ApplyStun();
        void RemoveStun();
    }

    [Serializable]
    public class StunEffect : BuffEffect
    {
        public override void OnApply(BuffInstance buff, GameObject target)
        {
            target.GetComponent<IStunnable>()?.ApplyStun();
        }

        public override void OnRemove(BuffInstance buff, GameObject target)
        {
            target.GetComponent<IStunnable>()?.RemoveStun();
        }
    }
}
