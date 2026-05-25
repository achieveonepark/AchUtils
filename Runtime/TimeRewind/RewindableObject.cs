using UnityEngine;

namespace AchieveOnePark.AchUtils.TimeRewind
{
    public class RewindableObject : MonoBehaviour, IRewindable
    {
        private TimeRewindSystem _rewindSystem;

        private void OnEnable()
        {
            _rewindSystem?.Register(this);
        }

        private void OnDisable()
        {
            _rewindSystem?.Unregister(this);
        }

        public void Bind(TimeRewindSystem rewindSystem)
        {
            if (_rewindSystem == rewindSystem) return;

            _rewindSystem?.Unregister(this);
            _rewindSystem = rewindSystem;
            if (isActiveAndEnabled)
                _rewindSystem?.Register(this);
        }

        public virtual object CaptureState()
        {
            return new TransformState
            {
                Position = transform.position,
                Rotation = transform.rotation,
                LocalScale = transform.localScale
            };
        }

        public virtual void RestoreState(object state)
        {
            if (state is not TransformState s) return;
            transform.position = s.Position;
            transform.rotation = s.Rotation;
            transform.localScale = s.LocalScale;
        }
    }
}
