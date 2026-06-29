using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Bool 상수 값과 BoolVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class BoolReference : AchReference<bool, BoolVariable>
    {
        public BoolReference()
        {
        }

        public BoolReference(bool value) : base(value)
        {
        }
    }
}
