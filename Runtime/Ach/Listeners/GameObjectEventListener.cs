using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class GameObjectUnityEvent : UnityEvent<GameObject>
    {
    }

    [AddComponentMenu("AchUtils/Ach/GameObject Event Listener")]
    public class GameObjectEventListener : AchEventListener<GameObject, GameObjectEvent, GameObjectUnityEvent>
    {
    }
}
