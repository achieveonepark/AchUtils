using UnityEngine;

namespace AchUtils.StatModifier
{
    public class StatSheetComponent : MonoBehaviour
    {
        public StatSheet Sheet { get; } = new();
    }
}
