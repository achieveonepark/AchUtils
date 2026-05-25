using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Camera
{
    public class CameraDirector : MonoBehaviour
    {
        public static CameraDirector Instance { get; private set; }

        [SerializeField] private UnityEngine.Camera _camera;

        private Coroutine _current;

        public UnityEngine.Camera Camera => _camera != null ? _camera : UnityEngine.Camera.main;
        public bool IsPlaying { get; private set; }

        public event Action OnSequenceCompleted;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void Play(CameraAction action)
        {
            Stop();
            _current = StartCoroutine(RunSingle(action));
        }

        public void PlaySequence(IEnumerable<CameraAction> actions)
        {
            Stop();
            _current = StartCoroutine(RunSequence(actions));
        }

        public void Stop()
        {
            if (_current != null)
            {
                StopCoroutine(_current);
                _current = null;
            }
            IsPlaying = false;
        }

        private IEnumerator RunSingle(CameraAction action)
        {
            IsPlaying = true;
            yield return action.Execute(this);
            IsPlaying = false;
            _current = null;
            OnSequenceCompleted?.Invoke();
        }

        private IEnumerator RunSequence(IEnumerable<CameraAction> actions)
        {
            IsPlaying = true;
            foreach (var action in actions)
                yield return action.Execute(this);
            IsPlaying = false;
            _current = null;
            OnSequenceCompleted?.Invoke();
        }
    }
}
