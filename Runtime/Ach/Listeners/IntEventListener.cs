using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Int Event Listener")]
    public class IntEventListener : AchEventListener<int, IntEvent, IntUnityEvent>
    {
    }
}
