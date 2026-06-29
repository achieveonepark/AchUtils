using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class Vector2UnityEvent : UnityEvent<Vector2>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Vector2 Event Listener")]
    public class Vector2EventListener : AchEventListener<Vector2, Vector2Event, Vector2UnityEvent>
    {
    }
}
