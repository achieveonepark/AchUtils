using UnityEngine;

namespace AchieveOnePark.AchUtils.ActionReplay
{
    public class ActionRecorder
    {
        private ReplayData _current;
        private float _startTime;

        public bool IsRecording { get; private set; }
        public ReplayData LastRecording { get; private set; }

        public void StartRecording() => StartRecording(Time.time);

        public void StartRecording(float currentTime)
        {
            if (IsRecording) return;
            _current = new ReplayData();
            _startTime = currentTime;
            IsRecording = true;
        }

        public ReplayData StopRecording() => StopRecording(Time.time);

        public ReplayData StopRecording(float currentTime)
        {
            if (!IsRecording) return null;
            IsRecording = false;
            _current.TotalDuration = currentTime - _startTime;
            LastRecording = _current.Clone();
            _current = null;
            return LastRecording;
        }

        public void Record(string key, Vector2 axis = default, bool pressed = true) =>
            Record(Time.time, key, axis, pressed);

        public void Record(float currentTime, string key, Vector2 axis = default, bool pressed = true)
        {
            if (!IsRecording) return;
            _current.Frames.Add(new InputFrame(currentTime - _startTime, key, axis, pressed));
        }

        public void RecordButton(string key, bool pressed) => Record(key, Vector2.zero, pressed);

        public void RecordAxis(string key, Vector2 axis) => Record(key, axis, true);
    }
}
