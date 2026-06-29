using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>Vector3 상수 값과 Vector3Variable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class Vector3Reference : AchReference<Vector3, Vector3Variable>
    {
        public Vector3Reference()
        {
        }

        public Vector3Reference(Vector3 value) : base(value)
        {
        }
    }
}
