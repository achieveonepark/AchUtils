using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.TimeRewind
{
    public class TimeRewindSystem : MonoBehaviour
    {
        public static TimeRewindSystem Instance { get; private set; }

        [SerializeField] private float _maxHistorySeconds = 5f;
        [SerializeField] private int _snapshotsPerSecond = 10;

        private readonly List<IRewindable> _targets = new();
        private readonly Queue<(float time, Dictionary<IRewindable, object> states)> _history = new();

        private float _recordInterval;
        private float _recordTimer;

        public bool IsRewinding { get; private set; }

        public event Action OnRewindStart;
        public event Action OnRewindStop;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            _recordInterval = 1f / Mathf.Max(1, _snapshotsPerSecond);
        }

        private void Update()
        {
            if (IsRewinding) return;

            _recordTimer += Time.deltaTime;
            if (_recordTimer >= _recordInterval)
            {
                _recordTimer -= _recordInterval;
                RecordSnapshot();
            }

            while (_history.Count > 0 && Time.time - _history.Peek().time > _maxHistorySeconds)
                _history.Dequeue();
        }

        private void RecordSnapshot()
        {
            if (_targets.Count == 0) return;
            var states = new Dictionary<IRewindable, object>(_targets.Count);
            foreach (var t in _targets)
                states[t] = t.CaptureState();
            _history.Enqueue((Time.time, states));
        }

        public void Register(IRewindable target)
        {
            if (!_targets.Contains(target)) _targets.Add(target);
        }

        public void Unregister(IRewindable target) => _targets.Remove(target);

        public void StartRewind()
        {
            IsRewinding = true;
            OnRewindStart?.Invoke();
        }

        public void StopRewind()
        {
            IsRewinding = false;
            OnRewindStop?.Invoke();
        }

        public void RewindTo(float secondsAgo)
        {
            float targetTime = Time.time - Mathf.Clamp(secondsAgo, 0f, _maxHistorySeconds);
            (float time, Dictionary<IRewindable, object> states)? best = null;
            float bestDiff = float.MaxValue;

            foreach (var snapshot in _history)
            {
                float diff = Mathf.Abs(snapshot.time - targetTime);
                if (diff < bestDiff) { bestDiff = diff; best = snapshot; }
            }

            if (!best.HasValue) return;

            foreach (var kvp in best.Value.states)
                kvp.Key.RestoreState(kvp.Value);
        }

        public void StepRewind(float deltaSeconds)
        {
            if (_history.Count == 0) return;
            var frames = new List<(float, Dictionary<IRewindable, object>)>(_history);

            float targetTime = Time.time - deltaSeconds;
            var best = frames[0];
            float bestDiff = Mathf.Abs(best.Item1 - targetTime);

            foreach (var snap in frames)
            {
                float diff = Mathf.Abs(snap.Item1 - targetTime);
                if (diff < bestDiff) { bestDiff = diff; best = snap; }
            }

            foreach (var kvp in best.Item2)
                kvp.Key.RestoreState(kvp.Value);
        }

        public float HistoryDuration =>
            _history.Count >= 2
                ? _history.Count / (float)_snapshotsPerSecond
                : 0f;
    }
}
