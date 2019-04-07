using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QMenus
{
    /// <summary> The way to align the tabs at the top of the tab group. </summary>
    [System.Serializable]
    public enum QMTabAlign
    {
        /// <summary> The tabs will be aligned to the center of the group. </summary>
        Center,
        /// <summary> The tabs will be aligned to the left of the group. </summary>
        Left,
        /// <summary> The tabs will be aligned to the right of the group. </summary>
        Right
    }

    /// <summary> Represents a group of tab windows, one of which can be open at a time. Tab groups are used in menus. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Tab Group")]
    [DisallowMultipleComponent]
    public class QMTabGroup : MonoBehaviour
    {
        [HideInInspector] public List<QMTabWindow> windows = new List<QMTabWindow>();
        [HideInInspector] public List<QMTab> tabs = new List<QMTab>();
        [HideInInspector] public QMTabWindow currentTab;
        public QMTabAlign tabAlign;
        public float tabYOffset;
        public float tabXSideOffset;
        public RectTransform content;
        public Animator animator;
        public QMWAnimationType animationType;
        [HideInInspector] public string animatorOpen;
        [HideInInspector] public string animatorClose;
        [HideInInspector] public string animatorParameter;
        [HideInInspector] public string animatorOpenTrigger;
        [HideInInspector] public string animatorCloseTrigger;
        [HideInInspector] public int animatorOpenInt;
        [HideInInspector] public int animatorCloseInt;
        [HideInInspector] public float animatorOpenFloat;
        [HideInInspector] public float animatorCloseFloat;
        public bool isOpen = true;

        protected void Awake()
        {
            // Retrieve all windows and tabs at runtime
            GetWindows();
            GetTabs();
            // Change state of tab group at runtime
            OverrideOpenState(isOpen);
            // Finds current tab at runtime
            int windowCount = windows.Count;
            for (int i = 0; i < windowCount; ++i)
            { if (windows[i].isOpen) { currentTab = windows[i]; break; } }
        }

        public void GetWindows()
        { windows = new List<QMTabWindow>(GetComponentsInChildren<QMTabWindow>()); }
        public void GetTabs()
        { tabs = new List<QMTab>(GetComponentsInChildren<QMTab>()); }

        /// <summary> Changes the current tab. </summary>
        /// <param name="pTab">Tab to switch to.</param>
        public void ChangeTab(QMTabWindow pTab)
        {
            if (!currentTab)
            {
                // Finds current tab
                int windowCount = windows.Count;
                for (int i = 0; i < windowCount; ++i)
                { if (windows[i].isOpen) { currentTab = windows[i]; break; } }
            }
            if (windows.Count == 0) GetWindows();
            if (tabs.Count == 0) GetTabs();

            if (!windows.Contains(pTab) || (currentTab && currentTab == pTab) || pTab == null) return;
            if (currentTab) currentTab.ChangeOpenState(false);
            currentTab = pTab;
            currentTab.ChangeOpenState(true);
        }

        public void OverrideTab(QMTabWindow pTab)
        {
            if (!currentTab)
            {
                // Finds current tab
                int windowCount = windows.Count;
                for (int i = 0; i < windowCount; ++i)
                { if (windows[i].isOpen) { currentTab = windows[i]; break; } }
            }
            if (windows.Count == 0) GetWindows();
            if (tabs.Count == 0) GetTabs();

            if (!windows.Contains(pTab) || (currentTab && currentTab == pTab) || pTab == null) return;
            if (currentTab) currentTab.OverrideOpenState(false);
            currentTab = pTab;
            currentTab.OverrideOpenState(true);
        }

        public void InspectorChangeTab(QMTabWindow pTab)
        {
            if (!currentTab)
            {
                // Finds current tab
                int windowCount = windows.Count;
                for (int i = 0; i < windowCount; ++i)
                { if (windows[i].isOpen) { currentTab = windows[i]; break; } }
            }
            if (windows.Count == 0) GetWindows();
            if (tabs.Count == 0) GetTabs();

            if (!windows.Contains(pTab) || (currentTab && currentTab == pTab) || pTab == null) return;
            if (currentTab) currentTab.InspectorChangeOpenState(false);
            currentTab = pTab;
            currentTab.InspectorChangeOpenState(true);
        }

        /// <summary> Changes the tab group's open state. </summary>
        /// <param name="pOpen">Whether or not the tab group is open.</param>
        public void ChangeOpenState(bool pOpen)
        {
            if (isOpen == pOpen) return;
            isOpen = pOpen;
            if (animationType == QMWAnimationType.ActiveState && content) content.gameObject.SetActive(isOpen);
            else if (animationType == QMWAnimationType.AnimatorBool && animator) animator.SetBool(animatorParameter, isOpen);
            else if (animationType == QMWAnimationType.AnimatorTrigger && animator) animator.SetTrigger(isOpen ? animatorOpenTrigger : animatorCloseTrigger);
            else if (animationType == QMWAnimationType.AnimatorInt && animator) animator.SetInteger(animatorParameter, isOpen ? animatorOpenInt : animatorCloseInt);
            else if (animationType == QMWAnimationType.AnimatorFloat && animator) animator.SetFloat(animatorParameter, isOpen ? animatorOpenFloat : animatorCloseFloat);
        }

        public void OverrideOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (animationType == QMWAnimationType.ActiveState && content) content.gameObject.SetActive(isOpen);
            else if (animationType == QMWAnimationType.AnimatorBool && animator) animator.SetBool(animatorParameter, isOpen);
            else if (animationType == QMWAnimationType.AnimatorTrigger && animator) animator.SetTrigger(isOpen ? animatorOpenTrigger : animatorCloseTrigger);
            else if (animationType == QMWAnimationType.AnimatorInt && animator) animator.SetInteger(animatorParameter, isOpen ? animatorOpenInt : animatorCloseInt);
            else if (animationType == QMWAnimationType.AnimatorFloat && animator) animator.SetFloat(animatorParameter, isOpen ? animatorOpenFloat : animatorCloseFloat);
            if (animator && animatorOpen.Length > 0 && animatorClose.Length > 0) animator.PlayInFixedTime(isOpen ? animatorOpen : animatorClose, 0, 1);
        }

        public void InspectorChangeOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (content) content.gameObject.SetActive(isOpen);
        }

        /// <summary> Aligns the tabs of the tab group with the currently selected alignment. </summary>
        public void AlignTabs()
        {
            if (windows.Count == 0) GetWindows();
            if (tabs.Count == 0) GetTabs();

            float tabsWidth = 0;
            int tabCount = tabs.Count;
            RectTransform tabTransform = null;
            // Find the total width.
            for (int i = 0; i < tabCount; ++i)
            { tabTransform = tabs[i].GetComponent<RectTransform>(); if (!tabTransform) continue; tabsWidth += tabTransform.rect.width; }
            // Align the tabs using the total width.
            float currentTabWidth = 0;
            for (int i = 0; i < tabCount; ++i)
            {
                tabTransform = tabs[i].GetComponent<RectTransform>();
                if (!tabTransform) continue;
                float tabWidth = tabTransform.rect.width;
                float tabHeight = tabTransform.rect.height;

                if (tabAlign == QMTabAlign.Center)
                {
                    tabTransform.anchorMin = new Vector2(0.5f, 1);
                    tabTransform.anchorMax = new Vector2(0.5f, 1);
                    tabTransform.pivot = new Vector2(0.5f, 1);
                    tabTransform.anchoredPosition = new Vector2(-tabsWidth / 2 + tabsWidth / 2 / tabCount + currentTabWidth, tabHeight + tabYOffset);
                    currentTabWidth += tabWidth;
                }
                else if (tabAlign == QMTabAlign.Left)
                {
                    tabTransform.anchorMin = new Vector2(0, 1);
                    tabTransform.anchorMax = new Vector2(0, 1);
                    tabTransform.pivot = new Vector2(0, 1);
                    tabTransform.anchoredPosition = new Vector2(currentTabWidth + tabXSideOffset, tabHeight + tabYOffset);
                    currentTabWidth += tabWidth;
                }
                else if (tabAlign == QMTabAlign.Right)
                {
                    currentTabWidth += tabWidth;
                    tabTransform.anchorMin = new Vector2(1, 1);
                    tabTransform.anchorMax = new Vector2(1, 1);
                    tabTransform.pivot = new Vector2(1, 1);
                    tabTransform.anchoredPosition = new Vector2(-tabsWidth + currentTabWidth - tabXSideOffset, tabHeight + tabYOffset);
                }
            }
        }
    }
}