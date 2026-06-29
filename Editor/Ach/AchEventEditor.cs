using AchUtils.Ach;
using UnityEditor;
using UnityEngine;

namespace AchUtils.Editor.Ach
{
    /// <summary>
    /// 값을 싣는 Ach 이벤트 인스펙터에 "구독자 비우기" 버튼을 더합니다.
    /// 플레이 중 이벤트 흐름을 디버깅할 때 유용합니다.
    /// </summary>
    [CustomEditor(typeof(AchEventBase), true)]
    public class AchEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawRuntimeControls();
        }

        protected virtual void DrawRuntimeControls()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            var achEvent = (AchEventBase)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime", EditorStyles.boldLabel);

            if (GUILayout.Button("Clear Subscribers"))
            {
                achEvent.ClearSubscribers();
            }
        }
    }

    /// <summary>
    /// VoidEvent 전용 인스펙터. 플레이 중 "Raise" 버튼으로 이벤트를 수동 발생시켜
    /// 리스너 연결을 즉석에서 테스트할 수 있습니다.
    /// </summary>
    [CustomEditor(typeof(VoidEvent))]
    public class VoidEventEditor : AchEventEditor
    {
        protected override void DrawRuntimeControls()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            var voidEvent = (VoidEvent)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime", EditorStyles.boldLabel);

            if (GUILayout.Button("Raise"))
            {
                voidEvent.Raise();
            }

            if (GUILayout.Button("Clear Subscribers"))
            {
                voidEvent.ClearSubscribers();
            }
        }
    }
}
