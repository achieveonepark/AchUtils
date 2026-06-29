using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Int 상수 값과 IntVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class IntReference : AchReference<int, IntVariable>
    {
        public IntReference()
        {
        }

        public IntReference(int value) : base(value)
        {
        }
    }
}
