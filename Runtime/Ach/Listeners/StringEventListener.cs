using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class StringUnityEvent : UnityEvent<string>
    {
    }

    [AddComponentMenu("AchUtils/Ach/String Event Listener")]
    public class StringEventListener : AchEventListener<string, StringEvent, StringUnityEvent>
    {
    }
}
