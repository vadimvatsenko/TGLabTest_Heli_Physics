using UnityEditor;

namespace InputSystem.Editor
{
    [CustomEditor(typeof(BaseHeliInput))]
    public class KeyBoardHeliInputDebug : UnityEditor.Editor
    {
        private BaseHeliInput _input;

        private void OnEnable() => _input = target as BaseHeliInput;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDebugUI();
            Repaint();
        }

        private void DrawDebugUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Input System", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++; // падінг
            EditorGUILayout.LabelField($"ThrottleInput: {_input.ThrottleInput}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"CollectiveInput: {_input.CollectiveInput}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"CyclicInput: {_input.CyclicInput}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"PedalInput: {_input.PedalInput}", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }
}