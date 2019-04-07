using UnityEngine;
using UnityEditor;

namespace QuantumTek.QMenus.Editors
{
    [CustomEditor(typeof(QMMenu))]
    public class QMMenuInspector : Editor
    {
        protected static QMMenu menu;

        public override void OnInspectorGUI()
        {
            menu = (QMMenu)target;
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open All")) { Undo.RecordObject(menu, "Open All"); menu.GetWindows(); menu.GetTabGroups(); menu.InspectorChangeAllOpenState(true); }
            if (GUILayout.Button("Close All")) { Undo.RecordObject(menu, "Close All"); menu.GetWindows(); menu.GetTabGroups(); menu.InspectorChangeAllOpenState(false); }
            if (GUILayout.Button("Open All Windows")) { Undo.RecordObject(menu, "Open All Windows"); menu.GetWindows(); menu.InspectorChangeWindowOpenState(true); }
            if (GUILayout.Button("Close All Windows")) { Undo.RecordObject(menu, "Close All Windows"); menu.GetWindows(); menu.InspectorChangeWindowOpenState(false); }
            if (GUILayout.Button("Open All Tab Groups")) { Undo.RecordObject(menu, "Open All Tab Groups"); menu.GetTabGroups(); menu.InspectorChangeTabGroupOpenState(true); }
            if (GUILayout.Button("Close All Tab Groups")) { Undo.RecordObject(menu, "Close All Tab Groups"); menu.GetTabGroups(); menu.InspectorChangeTabGroupOpenState(false); }
            GUILayout.EndHorizontal();
        }
    }
}