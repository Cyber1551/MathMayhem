using UnityEngine;
using TMPro;

namespace QuantumTek.QMenus
{
    /// <summary> Represents saves and loads a dropdown setting. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Save Dropdown")]
    [DisallowMultipleComponent]
    public class QMSaveDropdown : MonoBehaviour
    {
        public string settingName;
        public TMP_Dropdown reference;

        protected void Awake()
        {
            if (!reference || settingName.Length == 0) return;
            reference.value = PlayerPrefs.GetInt(settingName);
        }

        public void Save()
        {
            if (!reference || settingName.Length == 0) return;
            PlayerPrefs.SetInt(settingName, reference.value);
            PlayerPrefs.Save();
        }
    }
}