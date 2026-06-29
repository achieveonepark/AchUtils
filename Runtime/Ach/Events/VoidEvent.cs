using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>값을 싣지 않는 신호용 이벤트. "문이 열렸다", "레벨업 했다" 같은 순수 알림에 사용합니다.</summary>
    [CreateAssetMenu(menuName = "AchUtils/Ach/Events/Void Event", fileName = "NewVoidEvent")]
    public class VoidEvent : AchEventBase
    {
        public event Action OnRaised;

        public void Raise()
        {
            OnRaised?.Invoke();
        }

        public void Register(Action handler)
        {
            OnRaised += handler;
        }

        public void Unregister(Action handler)
        {
            OnRaised -= handler;
        }

        public override void ClearSubscribers()
        {
            OnRaised = null;
        }
    }
}
