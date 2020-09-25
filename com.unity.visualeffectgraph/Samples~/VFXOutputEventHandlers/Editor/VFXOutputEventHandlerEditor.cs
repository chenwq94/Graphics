using UnityEngine;
using UnityEngine.VFX.Utility;

namespace UnityEditor.VFX.Utility
{
    public abstract class VFXOutputEventHandlerEditor : Editor
    {
        protected SerializedProperty m_ExecuteInEditor;
        protected SerializedProperty m_OutputEvent;
        protected VFXOutputEventHandler m_TargetHandler;
        protected virtual void OnEnable()
        {
            m_TargetHandler = serializedObject.targetObject as VFXOutputEventHandler;
            m_OutputEvent = serializedObject.FindProperty(nameof(VFXOutputEventHandler.outputEvent));
            m_ExecuteInEditor = serializedObject.FindProperty(nameof(VFXOutputEventHandler.executeInEditor));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            if (m_TargetHandler.canExecuteInEditor)
                EditorGUILayout.PropertyField(m_ExecuteInEditor);
            else
                EditorGUILayout.HelpBox($"This VFX Output Event Handler cannot be previewed in edit mode. If you want to preview its behavior, please enter play mode.", MessageType.Info);
            
            EditorGUILayout.PropertyField(m_OutputEvent);

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }

        public void HelpBox(string title, string body)
        {
            using(new GUILayout.VerticalScope(Styles.helpBox))
            {
                GUILayout.Label(title, Styles.helpBoxTitle);
                GUILayout.Label(body, Styles.helpBoxBody);
            }
        }

        static class Styles
        {
            public static GUIStyle helpBox;
            public static GUIStyle helpBoxTitle;
            public static GUIStyle helpBoxBody;

            static Styles()
            {
                helpBox = new GUIStyle(EditorStyles.helpBox);
                helpBox.margin = new RectOffset(0, 0, 12, 0);
                helpBoxTitle = new GUIStyle(EditorStyles.boldLabel);
                helpBoxTitle.margin = new RectOffset(0, 0, 0, 4);

                helpBoxBody = new GUIStyle(EditorStyles.label);
                helpBoxBody.wordWrap = true;
                helpBoxBody.padding = new RectOffset(8, 0, 0, 0);
            }
        }
    }
}
