using UnityEngine;
using UnityEngine.UI;

namespace QuantumTek.QMenus
{
    /// <summary> Represents saves and loads a slider setting. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Save Slider")]
    [DisallowMultipleComponent]
    public class QMSaveSlider : MonoBehaviour
    {
        public string settingName;
        public Slider reference;

        protected void Awake()
        {
            if (!reference || settingName.Length == 0) return;
            reference.value = PlayerPrefs.GetFloat(settingName);
        }

        public void Save()
        {
            if (!reference || settingName.Length == 0) return;
            PlayerPrefs.SetFloat(settingName, reference.value);
            PlayerPrefs.Save();
        }
    }
}