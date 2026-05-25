using System;
using System.Collections.Generic;

namespace AchUtils.ActionReplay
{
    [Serializable]
    public class ReplayData
    {
        public float TotalDuration;
        public List<InputFrame> Frames = new();

        public ReplayData Clone()
        {
            return new ReplayData
            {
                TotalDuration = TotalDuration,
                Frames = new List<InputFrame>(Frames)
            };
        }
    }
}
