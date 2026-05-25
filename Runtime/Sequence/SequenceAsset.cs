using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    [CreateAssetMenu(menuName = "SS/Sequence/Sequence Asset", fileName = "NewSequence")]
    public class SequenceAsset : ScriptableObject
    {
        [SerializeReference] public List<SequenceStep> Steps = new();
    }
}
