using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.ActionReplay
{
    [Serializable]
    public struct InputFrame
    {
        public float Timestamp;
        public string ActionKey;
        public Vector2 Axis;
        public bool IsPressed;

        public InputFrame(float timestamp, string key, Vector2 axis, bool pressed)
        {
            Timestamp = timestamp;
            ActionKey = key;
            Axis = axis;
            IsPressed = pressed;
        }
    }
}
