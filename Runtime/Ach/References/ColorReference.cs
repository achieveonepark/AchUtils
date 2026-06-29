using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Color 상수 값과 ColorVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class ColorReference : AchReference<Color, ColorVariable>
    {
        public ColorReference()
        {
        }

        public ColorReference(Color value) : base(value)
        {
        }
    }
}
