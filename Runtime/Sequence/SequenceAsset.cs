using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Sequence
{
    [CreateAssetMenu(menuName = "AchUtils/Sequence/Sequence Asset", fileName = "NewSequence")]
    public class SequenceAsset : ScriptableObject
    {
        [SerializeReference] public List<SequenceStep> Steps = new();
    }
}
