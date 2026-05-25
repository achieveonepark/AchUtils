using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Buff
{
    [CreateAssetMenu(menuName = "SS/Buff/Buff Definition", fileName = "NewBuff")]
    public class BuffDefinition : ScriptableObject
    {
        public string BuffId;
        public string DisplayName;
        [TextArea] public string Description;
        public Sprite Icon;

        [Tooltip("-1 means permanent")]
        public float Duration = 5f;

        [Tooltip("0 means no ticking")]
        public float TickInterval = 1f;

        public int MaxStacks = 1;
        public bool RefreshDurationOnReapply = true;

        [SerializeReference] public List<BuffEffect> Effects = new();
    }
}
