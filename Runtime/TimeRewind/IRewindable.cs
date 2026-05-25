namespace AchieveOnePark.AchUtils.TimeRewind
{
    public interface IRewindable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}
