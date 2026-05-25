using System;
using UnityEngine;

namespace AchieveOnePark.AchUtils.ActionReplay
{
    public class ActionPlayer
    {
        private ReplayData _data;
        private float _speed = 1f;
        private float _elapsed;
        private int _nextFrameIndex;

        public bool IsPlaying { get; private set; }

        public event Action<InputFrame> OnFrame;
        public event Action OnCompleted;
        public event Action OnStopped;

        public void Play(ReplayData data, float speed = 1f)
        {
            if (data == null || data.Frames.Count == 0) return;
            Stop();
            _data = data;
            _speed = Mathf.Max(0.01f, speed);
            _elapsed = 0f;
            _nextFrameIndex = 0;
            IsPlaying = true;
        }

        public void Stop()
        {
            if (!IsPlaying) return;
            _data = null;
            IsPlaying = false;
            OnStopped?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (!IsPlaying || _data == null) return;

            _elapsed += Mathf.Max(0f, deltaTime) * _speed;

            while (_nextFrameIndex < _data.Frames.Count &&
                   _data.Frames[_nextFrameIndex].Timestamp <= _elapsed)
            {
                OnFrame?.Invoke(_data.Frames[_nextFrameIndex]);
                _nextFrameIndex++;
            }

            if (_nextFrameIndex < _data.Frames.Count) return;

            _data = null;
            IsPlaying = false;
            OnCompleted?.Invoke();
        }
    }
}
