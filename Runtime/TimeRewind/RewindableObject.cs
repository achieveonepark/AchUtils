using UnityEngine;

namespace AchieveOnePark.AchUtils.TimeRewind
{
    public class RewindableObject : MonoBehaviour, IRewindable
    {
        private void OnEnable() => TimeRewindSystem.Instance?.Register(this);
        private void OnDisable() => TimeRewindSystem.Instance?.Unregister(this);

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
