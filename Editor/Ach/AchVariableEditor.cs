using AchUtils.Ach;
using UnityEditor;
using UnityEngine;

namespace AchUtils.Editor.Ach
{
    /// <summary>
    /// 모든 Ach 변수 인스펙터에 현재 값 표시와 "초기값으로 리셋" 버튼을 더합니다.
    /// 플레이 중 값이 어떻게 변하는지 인스펙터에서 바로 확인하고 되돌릴 수 있습니다.
    /// </summary>
    [CustomEditor(typeof(AchVariable), true)]
    public class AchVariableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!Application.isPlaying)
            {
                return;
            }

            var variable = (AchVariable)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Current Value", variable.BoxedValue?.ToString() ?? "null");

            if (GUILayout.Button("Reset To Initial Value"))
            {
                variable.ResetToInitialValue();
            }
        }
    }
}
