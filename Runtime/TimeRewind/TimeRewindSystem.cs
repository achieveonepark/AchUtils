using System;
using System.Collections.Generic;

namespace AchieveOnePark.AchUtils.TimeRewind
{
    public class TimeRewindSystem
    {
        private readonly float _maxHistorySeconds;
        private readonly int _snapshotsPerSecond;
        private readonly List<IRewindable> _targets = new();
        private readonly Queue<(float time, Dictionary<IRewindable, object> states)> _history = new();

        private readonly float _recordInterval;
        private float _recordTimer;
        private float _time;

        public bool IsRewinding { get; private set; }

        public event Action OnRewindStart;
        public event Action OnRewindStop;

        public TimeRewindSystem(float maxHistorySeconds = 5f, int snapshotsPerSecond = 10)
        {
            _maxHistorySeconds = Max(0f, maxHistorySeconds);
            _snapshotsPerSecond = Max(1, snapshotsPerSecond);
            _recordInterval = 1f / _snapshotsPerSecond;
        }

        public void Tick(float deltaTime)
        {
            deltaTime = Max(0f, deltaTime);
            _time += deltaTime;
            if (IsRewinding) return;

            _recordTimer += deltaTime;
            if (_recordTimer >= _recordInterval)
            {
                _recordTimer -= _recordInterval;
                RecordSnapshot();
            }

            while (_history.Count > 0 && _time - _history.Peek().time > _maxHistorySeconds)
                _history.Dequeue();
        }

        private void RecordSnapshot()
        {
            if (_targets.Count == 0) return;
            var states = new Dictionary<IRewindable, object>(_targets.Count);
            foreach (var t in _targets)
                states[t] = t.CaptureState();
            _history.Enqueue((_time, states));
        }

        public void Register(IRewindable target)
        {
            if (target != null && !_targets.Contains(target))
                _targets.Add(target);
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
            float targetTime = _time - Clamp(secondsAgo, 0f, _maxHistorySeconds);
            (float time, Dictionary<IRewindable, object> states)? best = null;
            float bestDiff = float.MaxValue;

            foreach (var snapshot in _history)
            {
                float diff = Abs(snapshot.time - targetTime);
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

            float targetTime = _time - deltaSeconds;
            var best = frames[0];
            float bestDiff = Abs(best.Item1 - targetTime);

            foreach (var snap in frames)
            {
                float diff = Abs(snap.Item1 - targetTime);
                if (diff < bestDiff) { bestDiff = diff; best = snap; }
            }

            foreach (var kvp in best.Item2)
                kvp.Key.RestoreState(kvp.Value);
        }

        public float HistoryDuration =>
            _history.Count >= 2
                ? _history.Count / (float)_snapshotsPerSecond
                : 0f;

        private float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }

        private float Max(float a, float b) => a > b ? a : b;

        private int Max(int a, int b) => a > b ? a : b;

        private float Abs(float value) => value < 0f ? -value : value;
    }
}
