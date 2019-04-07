using UnityEngine;
using UnityEditor;

namespace QuantumTek.QMenus.Editors
{
    [CustomEditor(typeof(QMWindow))]
    public class QMWindowInspector : Editor
    {
        protected static QMWindow window;

        protected static string animatorOpen;
        protected static string animatorClose;
        protected static string animatorParameter;
        protected static string animatorOpenTrigger;
        protected static string animatorCloseTrigger;
        protected static int animatorOpenInt;
        protected static int animatorCloseInt;
        protected static float animatorOpenFloat;
        protected static float animatorCloseFloat;

        public override void OnInspectorGUI()
        {
            window = (QMWindow)target;
            base.OnInspectorGUI();
            EditorGUI.BeginChangeCheck();

            if (window.animationType != QMWAnimationType.ActiveState)
            {
                animatorOpen = EditorGUILayout.TextField("Animator Open", window.animatorOpen);
                animatorClose = EditorGUILayout.TextField("Animator Close", window.animatorClose);
                if (window.animationType != QMWAnimationType.AnimatorTrigger) animatorParameter = EditorGUILayout.TextField("Animator Parameter", window.animatorParameter);
            }

            if (window.animationType == QMWAnimationType.AnimatorTrigger)
            {
                animatorOpenTrigger = EditorGUILayout.TextField("Animator Open Trigger", window.animatorOpenTrigger);
                animatorCloseTrigger = EditorGUILayout.TextField("Animator Close Trigger", window.animatorCloseTrigger);
            }
            else if (window.animationType == QMWAnimationType.AnimatorInt)
            {
                animatorOpenInt = EditorGUILayout.IntField("Animator Open Int", window.animatorOpenInt);
                animatorCloseInt = EditorGUILayout.IntField("Animator Close Int", window.animatorCloseInt);
            }
            else if (window.animationType == QMWAnimationType.AnimatorFloat)
            {
                animatorOpenFloat = EditorGUILayout.FloatField("Animator Open Float", window.animatorOpenFloat);
                animatorCloseFloat = EditorGUILayout.FloatField("Animator Close Float", window.animatorCloseFloat);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(window, "Change Properties");
                window.animatorOpen = animatorOpen;
                window.animatorClose = animatorClose;
                window.animatorParameter = animatorParameter;
                window.animatorOpenTrigger = animatorOpenTrigger;
                window.animatorCloseTrigger = animatorCloseTrigger;
                window.animatorOpenInt = animatorOpenInt;
                window.animatorCloseInt = animatorCloseInt;
                window.animatorOpenFloat = animatorOpenFloat;
                window.animatorCloseFloat = animatorCloseFloat;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open")) { Undo.RecordObject(window, "Open"); window.InspectorChangeOpenState(true); }
            if (GUILayout.Button("Close")) { Undo.RecordObject(window, "Close"); window.InspectorChangeOpenState(false); }
            GUILayout.EndHorizontal();
        }
    }
}