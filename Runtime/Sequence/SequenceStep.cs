using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    [Serializable]
    public abstract class SequenceStep
    {
        public abstract IEnumerator Execute(SequenceRunner runner);
    }
}
