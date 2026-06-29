using System;
using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    [Serializable]
    public class ColorUnityEvent : UnityEvent<Color>
    {
    }

    [AddComponentMenu("AchUtils/Ach/Color Event Listener")]
    public class ColorEventListener : AchEventListener<Color, ColorEvent, ColorUnityEvent>
    {
    }
}
