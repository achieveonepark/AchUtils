using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>
    /// 모든 Ach 변수의 비제네릭 베이스. 타입에 상관없이 변수를 참조하거나
    /// 에디터에서 일괄 처리할 때 사용합니다.
    /// </summary>
    public abstract class AchVariable : ScriptableObject
    {
        /// <summary>변수가 담고 있는 값의 타입.</summary>
        public abstract Type ValueType { get; }

        /// <summary>박싱된 현재 값. 디버그/에디터 용도.</summary>
        public abstract object BoxedValue { get; }

        /// <summary>값을 초기값으로 되돌립니다. 에디터 버튼 등에서 호출합니다.</summary>
        public abstract void ResetToInitialValue();
    }

    /// <summary>
    /// 값 하나를 ScriptableObject 에셋으로 들고 다니는 Ach 변수.
    /// 씬·프리팹 간 강한 결합 없이 값을 공유하고, 값이 바뀌면 구독자에게 알립니다.
    /// </summary>
    /// <typeparam name="T">담을 값의 타입.</typeparam>
    public abstract class AchVariable<T> : AchVariable
    {
        [SerializeField]
        [Tooltip("플레이 시작 시 Value 가 이 값으로 초기화됩니다.")]
        private T initialValue;

        [SerializeField]
        [Tooltip("현재 값. 런타임에 변경되며 인스펙터에서 실시간으로 확인할 수 있습니다.")]
        private T value;

        [SerializeField]
        [Tooltip("플레이에 들어갈 때 Value 를 Initial Value 로 되돌립니다.")]
        private bool resetValueOnPlay = true;

        /// <summary>값이 바뀔 때마다 새 값과 함께 호출됩니다.</summary>
        public event Action<T> OnChanged;

        /// <summary>값이 바뀔 때 (이전 값, 새 값) 순서로 호출됩니다.</summary>
        public event Action<T, T> OnChangedWithHistory;

        public override Type ValueType => typeof(T);

        public override object BoxedValue => value;

        /// <summary>플레이 시작 시 적용되는 초기값.</summary>
        public T InitialValue
        {
            get => initialValue;
            set => initialValue = value;
        }

        /// <summary>
        /// 현재 값. 대입하면 값이 실제로 바뀐 경우에만 이벤트가 발생합니다.
        /// </summary>
        public T Value
        {
            get => value;
            set => SetValue(value);
        }

        /// <summary>
        /// 값을 설정합니다. 기존 값과 같으면 아무 일도 일어나지 않습니다.
        /// </summary>
        public void SetValue(T newValue)
        {
            if (AreEqual(value, newValue))
            {
                return;
            }

            T old = value;
            value = newValue;
            Raise(old, newValue);
        }

        /// <summary>
        /// 값이 같더라도 강제로 이벤트를 발생시키며 설정합니다.
        /// </summary>
        public void SetValueAndForceRaise(T newValue)
        {
            T old = value;
            value = newValue;
            Raise(old, newValue);
        }

        /// <summary>Value 를 InitialValue 로 되돌립니다.</summary>
        public override void ResetToInitialValue()
        {
            SetValue(initialValue);
        }

        protected virtual bool AreEqual(T a, T b)
        {
            return EqualityComparer<T>.Default.Equals(a, b);
        }

        private void Raise(T oldValue, T newValue)
        {
            OnChanged?.Invoke(newValue);
            OnChangedWithHistory?.Invoke(oldValue, newValue);
        }

        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
            UnityEditor.EditorApplication.playModeStateChanged += HandlePlayModeChanged;
#else
            if (resetValueOnPlay)
            {
                value = initialValue;
            }
#endif
        }

        protected virtual void OnDisable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
#endif
        }

#if UNITY_EDITOR
        private void HandlePlayModeChanged(UnityEditor.PlayModeStateChange change)
        {
            // 도메인 리로드를 끈 프로젝트에서도 매 플레이마다 깨끗한 상태로 시작하도록 보정합니다.
            if (change == UnityEditor.PlayModeStateChange.ExitingEditMode && resetValueOnPlay)
            {
                value = initialValue;
            }
            else if (change == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                OnChanged = null;
                OnChangedWithHistory = null;
            }
        }
#endif
    }
}
