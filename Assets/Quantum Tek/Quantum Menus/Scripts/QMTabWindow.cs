using UnityEngine;

namespace QuantumTek.QMenus
{
    /// <summary> Represents a UI tab window, used in a tab group. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Tab Window")]
    [DisallowMultipleComponent]
    public class QMTabWindow : MonoBehaviour
    {
        [HideInInspector] public QMTab tab;
        public RectTransform content;
        public bool isOpen;

        protected void Awake()
        {
            // Retrieve tab at runtime
            tab = GetComponentInChildren<QMTab>();
            if (tab) tab.OverrideOpenState(isOpen);
        }

        /// <summary> Changes the tab window's open state. </summary>
        /// <param name="pOpen">Whether or not the tab window is open.</param>
        public void ChangeOpenState(bool pOpen)
        {
            if (isOpen == pOpen) return;
            isOpen = pOpen;
            if (content) content.gameObject.SetActive(isOpen);
            if (!tab) tab = GetComponentInChildren<QMTab>();
            if (tab) tab.ChangeOpenState(isOpen);
        }

        public void OverrideOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (content) content.gameObject.SetActive(isOpen);
            if (!tab) tab = GetComponentInChildren<QMTab>();
            if (tab) tab.OverrideOpenState(isOpen);
        }

        public void InspectorChangeOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (content) content.gameObject.SetActive(isOpen);
            if (!tab) tab = GetComponentInChildren<QMTab>();
            if (tab) tab.InspectorChangeOpenState(isOpen);
        }
    }
}