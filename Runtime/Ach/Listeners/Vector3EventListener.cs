using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class Vector3UnityEvent : UnityEvent<Vector3>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Vector3 Event Listener")]
    public class Vector3EventListener : AchEventListener<Vector3, Vector3Event, Vector3UnityEvent>
    {
    }
}
