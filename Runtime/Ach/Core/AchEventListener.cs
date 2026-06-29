using UnityEngine;
using UnityEngine.Events;

namespace AchUtils.Ach
{
    /// <summary>
    /// Ach 이벤트를 구독해 인스펙터에서 연결한 UnityEvent 로 반응하는 컴포넌트.
    /// 코드를 작성하지 않고도 이벤트 → 동작을 디자이너가 직접 연결할 수 있습니다.
    /// </summary>
    /// <typeparam name="T">이벤트 값의 타입.</typeparam>
    /// <typeparam name="TEvent">구독할 Ach 이벤트 타입.</typeparam>
    /// <typeparam name="TResponse">값을 받는 직렬화 가능한 UnityEvent 타입.</typeparam>
    public abstract class AchEventListener<T, TEvent, TResponse> : MonoBehaviour
        where TEvent : AchEvent<T>
        where TResponse : UnityEvent<T>
    {
        [SerializeField]
        [Tooltip("구독할 Ach 이벤트 에셋.")]
        private TEvent achEvent;

        [SerializeField]
        [Tooltip("이벤트가 발생하면 호출할 동작.")]
        private TResponse response;

        /// <summary>구독 중인 이벤트. 런타임에 교체하면 자동으로 재구독합니다.</summary>
        public TEvent Event
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

        protected virtual void OnEnable()
        {
            if (achEvent != null)
            {
                achEvent.Register(OnEventRaised);
            }
        }

        protected virtual void OnDisable()
        {
            if (achEvent != null)
            {
                achEvent.Unregister(OnEventRaised);
            }
        }

        protected virtual void OnEventRaised(T value)
        {
            response?.Invoke(value);
        }
    }
}
