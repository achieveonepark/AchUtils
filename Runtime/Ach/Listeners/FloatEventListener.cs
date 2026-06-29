using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Float Event Listener")]
    public class FloatEventListener : AchEventListener<float, FloatEvent, FloatUnityEvent>
    {
    }
}
