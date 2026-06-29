using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Vector2 상수 값과 Vector2Variable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class Vector2Reference : AchReference<Vector2, Vector2Variable>
    {
        public Vector2Reference()
        {
        }

        public Vector2Reference(Vector2 value) : base(value)
        {
        }
    }
}
