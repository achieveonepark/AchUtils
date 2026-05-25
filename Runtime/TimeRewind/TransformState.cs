using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.TimeRewind
{
    [Serializable]
    public struct TransformState
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 LocalScale;
    }
}
