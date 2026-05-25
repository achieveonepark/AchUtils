using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.ActionReplay
{
    public class ActionPlayer : MonoBehaviour
    {
        private Coroutine _coroutine;

        public bool IsPlaying { get; private set; }

        public event Action<InputFrame> OnFrame;
        public event Action OnCompleted;
        public event Action OnStopped;

        public void Play(ReplayData data, float speed = 1f)
        {
            if (data == null || data.Frames.Count == 0) return;
            Stop();
            _coroutine = StartCoroutine(Playback(data, Mathf.Max(0.01f, speed)));
        }

        public void Stop()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
                IsPlaying = false;
                OnStopped?.Invoke();
            }
        }

        private IEnumerator Playback(ReplayData data, float speed)
        {
            IsPlaying = true;
            float startTime = Time.time;

            foreach (var frame in data.Frames)
            {
                float targetTime = frame.Timestamp / speed;
                while (Time.time - startTime < targetTime)
                    yield return null;

                OnFrame?.Invoke(frame);
            }

            IsPlaying = false;
            _coroutine = null;
            OnCompleted?.Invoke();
        }
    }
}
