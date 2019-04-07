using UnityEngine;
using UnityEditor;

namespace QuantumTek.QMenus.Editors
{
    [CustomEditor(typeof(QMTab))]
    public class QMTabInspector : Editor
    {
        protected static QMTab tab;

        protected static Sprite openSprite;
        protected static Sprite closeSprite;
        protected static Color openColor;
        protected static Color closeColor;
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
            tab = (QMTab)target;
            base.OnInspectorGUI();
            EditorGUI.BeginChangeCheck();

            if (tab.animationType != QMTAnimationType.Sprite && tab.animationType != QMTAnimationType.Color)
            {
                animatorOpen = EditorGUILayout.TextField("Animator Open", tab.animatorOpen);
                animatorClose = EditorGUILayout.TextField("Animator Close", tab.animatorClose);
                if (tab.animationType != QMTAnimationType.AnimatorTrigger) animatorParameter = EditorGUILayout.TextField("Animator Parameter", tab.animatorParameter);
            }

            if (tab.animationType == QMTAnimationType.Sprite)
            {
                openSprite = (Sprite)EditorGUILayout.ObjectField("Open Sprite", tab.openSprite, typeof(Sprite), false);
                closeSprite = (Sprite)EditorGUILayout.ObjectField("Close Sprite", tab.closeSprite, typeof(Sprite), false);
            }
            else if (tab.animationType == QMTAnimationType.Color)
            {
                openColor = EditorGUILayout.ColorField("Open Color", tab.openColor);
                closeColor = EditorGUILayout.ColorField("Close Color", tab.closeColor);
            }
            else if (tab.animationType == QMTAnimationType.AnimatorTrigger)
            {
                animatorOpenTrigger = EditorGUILayout.TextField("Animator Open Trigger", tab.animatorOpenTrigger);
                animatorCloseTrigger = EditorGUILayout.TextField("Animator Close Trigger", tab.animatorCloseTrigger);
            }
            else if (tab.animationType == QMTAnimationType.AnimatorInt)
            {
                animatorOpenInt = EditorGUILayout.IntField("Animator Open Int", tab.animatorOpenInt);
                animatorCloseInt = EditorGUILayout.IntField("Animator Close Int", tab.animatorCloseInt);
            }
            else if (tab.animationType == QMTAnimationType.AnimatorFloat)
            {
                animatorOpenFloat = EditorGUILayout.FloatField("Animator Open Float", tab.animatorOpenFloat);
                animatorCloseFloat = EditorGUILayout.FloatField("Animator Close Float", tab.animatorCloseFloat);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tab, "Change Properties");
                tab.animatorOpen = animatorOpen;
                tab.animatorClose = animatorClose;
                tab.animatorParameter = animatorParameter;
                tab.animatorOpenTrigger = animatorOpenTrigger;
                tab.animatorCloseTrigger = animatorCloseTrigger;
                tab.animatorOpenInt = animatorOpenInt;
                tab.animatorCloseInt = animatorCloseInt;
                tab.animatorOpenFloat = animatorOpenFloat;
                tab.animatorCloseFloat = animatorCloseFloat;
            }
        }
    }
}