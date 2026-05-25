using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.ActionReplay
{
    public class ActionRecorder : MonoBehaviour
    {
        private ReplayData _current;
        private float _startTime;

        public bool IsRecording { get; private set; }
        public ReplayData LastRecording { get; private set; }

        public void StartRecording()
        {
            if (IsRecording) return;
            _current = new ReplayData();
            _startTime = Time.time;
            IsRecording = true;
        }

        public ReplayData StopRecording()
        {
            if (!IsRecording) return null;
            IsRecording = false;
            _current.TotalDuration = Time.time - _startTime;
            LastRecording = _current.Clone();
            _current = null;
            return LastRecording;
        }

        public void Record(string key, Vector2 axis = default, bool pressed = true)
        {
            if (!IsRecording) return;
            _current.Frames.Add(new InputFrame(Time.time - _startTime, key, axis, pressed));
        }

        public void RecordButton(string key, bool pressed) => Record(key, Vector2.zero, pressed);

        public void RecordAxis(string key, Vector2 axis) => Record(key, axis, true);
    }
}
