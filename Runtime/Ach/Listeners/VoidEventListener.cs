using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    /// <summary>VoidEvent 를 구독해 인스펙터에서 연결한 UnityEvent 로 반응하는 컴포넌트.</summary>
    [AddComponentMenu("AchUtils/Ach/Void Event Listener")]
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("구독할 Void 이벤트 에셋.")]
        private VoidEvent achEvent;

        [SerializeField]
        [Tooltip("이벤트가 발생하면 호출할 동작.")]
        private UnityEvent response;

        public VoidEvent Event
        {
            get => achEvent;
            set
            {
                if (achEvent == value)
                {
                    return;
                }

                if (isActiveAndEnabled && achEvent != null)
                {
                    achEvent.Unregister(OnEventRaised);
                }

                achEvent = value;

                if (isActiveAndEnabled && achEvent != null)
                {
                    achEvent.Register(OnEventRaised);
                }
            }
        }

        private void OnEnable()
        {
            if (achEvent != null)
            {
                achEvent.Register(OnEventRaised);
            }
        }

        private void OnDisable()
        {
            if (achEvent != null)
            {
                achEvent.Unregister(OnEventRaised);
            }
        }

        private void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}
