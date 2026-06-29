using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>GameObject 상수 값과 GameObjectVariable 에셋 중 하나를 인스펙터에서 선택해 쓰는 참조.</summary>
    [Serializable]
    public class GameObjectReference : AchReference<GameObject, GameObjectVariable>
    {
        public GameObjectReference()
        {
        }

        public GameObjectReference(GameObject value) : base(value)
        {
        }
    }
}
