using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>String 상수 값과 StringVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class StringReference : AchReference<string, StringVariable>
    {
        public StringReference()
        {
        }

        public StringReference(string value) : base(value)
        {
        }
    }
}
