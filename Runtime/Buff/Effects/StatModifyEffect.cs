using System;
using UnityEngine;
using AchUtils.StatModifier;

namespace AchUtils.Buff
{
    [Serializable]
    public class StatModifyEffect : BuffEffect
    {
        public string StatKey;
        public ModifierType ModifierType;
        public float Value;

        private string SourceId(BuffInstance buff) => $"buff_{buff.Definition.BuffId}_{GetHashCode()}";

        public override void OnApply(BuffInstance buff, GameObject target)
        {
            var sheet = target.GetComponent<StatSheetComponent>();
            if (sheet == null) return;
            sheet.Sheet.AddModifier(StatKey, new StatModifier.StatModifier(SourceId(buff), ModifierType, Value));
        }

        public override void OnRemove(BuffInstance buff, GameObject target)
        {
            var sheet = target.GetComponent<StatSheetComponent>();
            sheet?.Sheet.RemoveModifier(StatKey, SourceId(buff));
        }

        public override void OnStackAdded(BuffInstance buff, GameObject target, int newStack)
        {
            var sheet = target.GetComponent<StatSheetComponent>();
            if (sheet == null) return;
            sheet.Sheet.RemoveModifier(StatKey, SourceId(buff));
            sheet.Sheet.AddModifier(StatKey, new StatModifier.StatModifier(SourceId(buff), ModifierType, Value * newStack));
        }
    }
}
