using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>
    /// 모든 Ach 이벤트의 비제네릭 베이스. 에디터에서 타입에 상관없이
    /// 이벤트를 다루기 위한 공통 진입점입니다.
    /// </summary>
    public abstract class AchEventBase : ScriptableObject
    {
        /// <summary>등록된 모든 구독자를 제거합니다.</summary>
        public abstract void ClearSubscribers();

#if UNITY_EDITOR
        protected virtual void OnEnable()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
            UnityEditor.EditorApplication.playModeStateChanged += HandlePlayModeChanged;
        }

        protected virtual void OnDisable()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
        }

        private void HandlePlayModeChanged(UnityEditor.PlayModeStateChange change)
        {
            // 플레이가 끝나면 이전 세션의 구독자가 남아 메모리 누수·중복 호출을 일으키지 않도록 정리합니다.
            if (change == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                ClearSubscribers();
            }
        }
#endif
    }

    /// <summary>
    /// 값을 실어 보내는 ScriptableObject 이벤트. 발신자와 수신자가 서로를 몰라도
    /// 에셋 하나를 매개로 느슨하게 연결됩니다.
    /// </summary>
    /// <typeparam name="T">이벤트로 전달할 값의 타입.</typeparam>
    public abstract class AchEvent<T> : AchEventBase
    {
        [SerializeField]
        [Tooltip("마지막으로 Raise 된 값. 디버그용으로 인스펙터에 표시됩니다.")]
        private T lastValue;

        /// <summary>이벤트가 발생할 때 전달된 값과 함께 호출됩니다.</summary>
        public event Action<T> OnRaised;

        /// <summary>마지막으로 전달된 값.</summary>
        public T LastValue => lastValue;

        /// <summary>구독자에게 값을 전달하며 이벤트를 발생시킵니다.</summary>
        public void Raise(T value)
        {
            lastValue = value;
            OnRaised?.Invoke(value);
        }

        /// <summary>핸들러를 등록합니다.</summary>
        public void Register(Action<T> handler)
        {
            OnRaised += handler;
        }

        /// <summary>핸들러를 해제합니다.</summary>
        public void Unregister(Action<T> handler)
        {
            OnRaised -= handler;
        }

        public override void ClearSubscribers()
        {
            OnRaised = null;
        }
    }
}
