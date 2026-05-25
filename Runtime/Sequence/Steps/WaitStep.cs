using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Sequence
{
    [Serializable]
    public class WaitStep : SequenceStep
    {
        public float Duration = 1f;

        public override IEnumerator Execute(SequenceRunner runner)
        {
            yield return new WaitForSeconds(Duration);
        }
    }
}
