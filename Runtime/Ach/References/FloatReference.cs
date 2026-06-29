using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Float 상수 값과 FloatVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class FloatReference : AchReference<float, FloatVariable>
    {
        public FloatReference()
        {
        }

        public FloatReference(float value) : base(value)
        {
        }
    }
}
