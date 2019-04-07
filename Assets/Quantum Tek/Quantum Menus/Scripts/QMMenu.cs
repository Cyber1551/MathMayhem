using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumTek.QMenus
{
    /// <summary> Represents a UI menu, made of several windows and or tab groups, which can all be opened/closed individually or as a whole.. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Menu")]
    [DisallowMultipleComponent]
    public class QMMenu : MonoBehaviour
    {
        [HideInInspector] public List<QMWindow> windows = new List<QMWindow>();
        [HideInInspector] public List<QMTabGroup> tabGroups = new List<QMTabGroup>();
        public RectTransform background;
        public QMBar loadingProgress;

        protected void Awake()
        {
            // Retrieve all windows and tab groups at runtime
            GetWindows();
            GetTabGroups();
        }

        /// <summary> Quits the application. </summary>
        public void Quit()
        { Application.Quit(); }

        /// <summary> Loads a scene while showing a loading screen. </summary>
        /// <param name="pBuildIndex">The build index of the scene.</param>
        public void LoadScene(int pBuildIndex)
        {
            if (pBuildIndex < 0 || pBuildIndex >= SceneManager.sceneCountInBuildSettings) { Debug.LogWarning("Tried to load the scene with build index " + pBuildIndex + ", but the build index was out of range."); return; }
            StartCoroutine(LoadSceneAsync(pBuildIndex));
        }
        /// <summary> Loads a scene while showing a loading screen. </summary>
        /// <param name="pSceneName">The name of the scene.</param>
        public void LoadScene(string pSceneName)
        {
            if (pSceneName.Length == 0) { Debug.LogWarning("Tried to load a scene with no name."); return; }
            StartCoroutine(LoadSceneAsync(pSceneName));
        }

        protected IEnumerator LoadSceneAsync(int pBuildIndex)
        {
            // Start loading the scene.
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(pBuildIndex);

            // Update loading graphic.
            while (!loadOperation.isDone)
            {
                // Get load progress, dividing by 0.9 because that is where it stops.
                float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

                if (loadingProgress) loadingProgress.ChangeFill(loadProgress);

                yield return null;
            }
        }
        protected IEnumerator LoadSceneAsync(string pSceneName)
        {
            // Start loading the scene.
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(pSceneName);

            // Update loading graphic.
            while (!loadOperation.isDone)
            {
                // Get load progress, dividing by 0.9 because that is where it stops.
                float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

                if (loadingProgress) loadingProgress.ChangeFill(loadProgress);

                yield return null;
            }
        }

        public void GetWindows()
        { windows = new List<QMWindow>(GetComponentsInChildren<QMWindow>()); }
        public void GetTabGroups()
        { tabGroups = new List<QMTabGroup>(GetComponentsInChildren<QMTabGroup>()); }

        /// <summary> Shows/hides the menu background. </summary>
        /// <param name="pShown">Whether or not it should be shown.</param>
        public void ToggleBackground(bool pShown)
        {
            if (!background || background.gameObject.activeSelf == pShown) return;
            background.gameObject.SetActive(pShown);
        }

        /// <summary> Changes the open state of all windows and tab groups. </summary>
        /// <param name="pOpen">Whether or not they are open.</param>
        public void ChangeAllOpenState(bool pOpen)
        {
            if (windows.Count == 0) GetWindows();
            if (tabGroups.Count == 0) GetTabGroups();
            int windowCount = windows.Count;
            int groupCount = tabGroups.Count;

            for (int i = 0; i < windowCount; ++i)
            { windows[i].ChangeOpenState(pOpen); }
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].ChangeOpenState(pOpen); }
        }
        /// <summary> Changes the open state of all windows. </summary>
        /// <param name="pOpen">Whether or not they are open.</param>
        public void ChangeWindowOpenState(bool pOpen)
        {
            int windowCount = windows.Count;
            for (int i = 0; i < windowCount; ++i)
            { windows[i].ChangeOpenState(pOpen); }
        }
        /// <summary> Changes the open state of all tab groups. </summary>
        /// <param name="pOpen">Whether or not they are open.</param>
        public void ChangeTabGroupOpenState(bool pOpen)
        {
            int groupCount = tabGroups.Count;
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].ChangeOpenState(pOpen); }
        }

        public void InspectorChangeAllOpenState(bool pOpen)
        {
            if (windows.Count == 0) GetWindows();
            if (tabGroups.Count == 0) GetTabGroups();
            int windowCount = windows.Count;
            int groupCount = tabGroups.Count;

            for (int i = 0; i < windowCount; ++i)
            { windows[i].isOpen = pOpen; windows[i].content.gameObject.SetActive(pOpen); }
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].isOpen = pOpen; tabGroups[i].content.gameObject.SetActive(pOpen); }
        }
        public void InspectorChangeWindowOpenState(bool pOpen)
        {
            int windowCount = windows.Count;
            for (int i = 0; i < windowCount; ++i)
            { windows[i].isOpen = pOpen; windows[i].content.gameObject.SetActive(pOpen); }
        }
        public void InspectorChangeTabGroupOpenState(bool pOpen)
        {
            int groupCount = tabGroups.Count;
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].isOpen = pOpen; tabGroups[i].content.gameObject.SetActive(pOpen); }
        }

        public void OverrideAllOpenState(bool pOpen)
        {
            if (windows.Count == 0) GetWindows();
            if (tabGroups.Count == 0) GetTabGroups();
            int windowCount = windows.Count;
            int groupCount = tabGroups.Count;

            for (int i = 0; i < windowCount; ++i)
            { windows[i].OverrideOpenState(pOpen); }
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].OverrideOpenState(pOpen); }
        }
        public void OverrideWindowOpenState(bool pOpen)
        {
            int windowCount = windows.Count;
            for (int i = 0; i < windowCount; ++i)
            { windows[i].OverrideOpenState(pOpen); }
        }
        public void OverrideTabGroupOpenState(bool pOpen)
        {
            int groupCount = tabGroups.Count;
            for (int i = 0; i < groupCount; ++i)
            { tabGroups[i].OverrideOpenState(pOpen); }
        }
    }
}