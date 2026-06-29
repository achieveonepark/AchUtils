using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>
    /// 런타임 동안 공유되는 컬렉션을 ScriptableObject 에셋으로 들고 다닙니다.
    /// 예: 살아 있는 적 목록, 활성화된 스폰 포인트 목록 등. 항목이 추가·제거될 때
    /// 이벤트로 알려 줍니다.
    /// </summary>
    /// <typeparam name="T">담을 항목의 타입.</typeparam>
    public abstract class AchCollection<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField]
        [Tooltip("플레이 시작 시 컬렉션을 비웁니다.")]
        private bool clearOnPlay = true;

        [SerializeField]
        private List<T> items = new List<T>();

        /// <summary>항목이 추가될 때 호출됩니다.</summary>
        public event Action<T> OnAdded;

        /// <summary>항목이 제거될 때 호출됩니다.</summary>
        public event Action<T> OnRemoved;

        /// <summary>컬렉션이 비워질 때 호출됩니다.</summary>
        public event Action OnCleared;

        /// <summary>현재 항목 수.</summary>
        public int Count => items.Count;

        /// <summary>인덱스로 항목에 접근합니다.</summary>
        public T this[int index] => items[index];

        /// <summary>항목을 추가하고 OnAdded 를 발생시킵니다.</summary>
        public void Add(T item)
        {
            items.Add(item);
            OnAdded?.Invoke(item);
        }

        /// <summary>아직 없을 때만 추가합니다. 추가되면 true.</summary>
        public bool AddIfMissing(T item)
        {
            if (items.Contains(item))
            {
                return false;
            }

            Add(item);
            return true;
        }

        /// <summary>항목을 제거하고 OnRemoved 를 발생시킵니다. 제거되면 true.</summary>
        public bool Remove(T item)
        {
            if (!items.Remove(item))
            {
                return false;
            }

            OnRemoved?.Invoke(item);
            return true;
        }

        /// <summary>포함 여부를 확인합니다.</summary>
        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        /// <summary>모든 항목을 제거하고 OnCleared 를 발생시킵니다.</summary>
        public void Clear()
        {
            items.Clear();
            OnCleared?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
            UnityEditor.EditorApplication.playModeStateChanged += HandlePlayModeChanged;
#else
            if (clearOnPlay)
            {
                items.Clear();
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
            if (change == UnityEditor.PlayModeStateChange.ExitingEditMode && clearOnPlay)
            {
                items.Clear();
            }
            else if (change == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                OnAdded = null;
                OnRemoved = null;
                OnCleared = null;
            }
        }
#endif
    }
}
