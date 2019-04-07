using UnityEngine;
using UnityEditor;

namespace QuantumTek.QMenus.Editors
{
    [CustomEditor(typeof(QMTabGroup))]
    public class QMTabGroupInspector : Editor
    {
        protected static QMTabGroup tabGroup;

        protected static QMTabWindow tabToChange;

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
            tabGroup = (QMTabGroup)target;
            QMTabAlign align = tabGroup.tabAlign;
            base.OnInspectorGUI();
            if (align != tabGroup.tabAlign)
            {
                Undo.RecordObject(tabGroup, "Align Tabs");
                tabGroup.GetWindows();
                tabGroup.GetTabs();
                tabGroup.AlignTabs();
            }

            EditorGUI.BeginChangeCheck();

            if (tabGroup.animationType != QMWAnimationType.ActiveState)
            {
                animatorOpen = EditorGUILayout.TextField("Animator Open", tabGroup.animatorOpen);
                animatorClose = EditorGUILayout.TextField("Animator Close", tabGroup.animatorClose);
                if (tabGroup.animationType != QMWAnimationType.AnimatorTrigger) animatorParameter = EditorGUILayout.TextField("Animator Parameter", tabGroup.animatorParameter);
            }

            if (tabGroup.animationType == QMWAnimationType.AnimatorTrigger)
            {
                animatorOpenTrigger = EditorGUILayout.TextField("Animator Open Trigger", tabGroup.animatorOpenTrigger);
                animatorCloseTrigger = EditorGUILayout.TextField("Animator Close Trigger", tabGroup.animatorCloseTrigger);
            }
            else if (tabGroup.animationType == QMWAnimationType.AnimatorInt)
            {
                animatorOpenInt = EditorGUILayout.IntField("Animator Open Int", tabGroup.animatorOpenInt);
                animatorCloseInt = EditorGUILayout.IntField("Animator Close Int", tabGroup.animatorCloseInt);
            }
            else if (tabGroup.animationType == QMWAnimationType.AnimatorFloat)
            {
                animatorOpenFloat = EditorGUILayout.FloatField("Animator Open Float", tabGroup.animatorOpenFloat);
                animatorCloseFloat = EditorGUILayout.FloatField("Animator Close Float", tabGroup.animatorCloseFloat);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tabGroup, "Change Properties");
                tabGroup.animatorOpen = animatorOpen;
                tabGroup.animatorClose = animatorClose;
                tabGroup.animatorParameter = animatorParameter;
                tabGroup.animatorOpenTrigger = animatorOpenTrigger;
                tabGroup.animatorCloseTrigger = animatorCloseTrigger;
                tabGroup.animatorOpenInt = animatorOpenInt;
                tabGroup.animatorCloseInt = animatorCloseInt;
                tabGroup.animatorOpenFloat = animatorOpenFloat;
                tabGroup.animatorCloseFloat = animatorCloseFloat;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open")) { Undo.RecordObject(tabGroup, "Open"); tabGroup.InspectorChangeOpenState(true); }
            if (GUILayout.Button("Close")) { Undo.RecordObject(tabGroup, "Close"); tabGroup.InspectorChangeOpenState(false); }
            if (GUILayout.Button("Align Tabs")) { Undo.RecordObject(tabGroup, "Align Tabs"); tabGroup.GetTabs(); tabGroup.AlignTabs(); }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            tabToChange = (QMTabWindow)EditorGUILayout.ObjectField(tabToChange, typeof(QMTabWindow), true);
            if (GUILayout.Button("Change Tab")) { Undo.RecordObject(tabGroup, "Change Tab"); tabGroup.GetWindows(); tabGroup.GetTabs(); tabGroup.InspectorChangeTab(tabToChange); }
            GUILayout.EndHorizontal();
        }
    }
}