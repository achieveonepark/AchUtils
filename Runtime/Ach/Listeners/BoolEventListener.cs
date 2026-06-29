using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Bool Event Listener")]
    public class BoolEventListener : AchEventListener<bool, BoolEvent, BoolUnityEvent>
    {
    }
}
