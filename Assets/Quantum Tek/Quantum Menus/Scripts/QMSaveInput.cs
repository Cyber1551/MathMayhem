using UnityEngine;
using TMPro;

namespace QuantumTek.QMenus
{
    /// <summary> Represents saves and loads an input setting. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Save Input")]
    [DisallowMultipleComponent]
    public class QMSaveInput : MonoBehaviour
    {
        public string settingName;
        public TMP_InputField reference;

        protected void Awake()
        {
            if (!reference || settingName.Length == 0) return;
            reference.text = PlayerPrefs.GetString(settingName);
        }

        public void Save()
        {
            if (!reference || settingName.Length == 0) return;
            PlayerPrefs.SetString(settingName, reference.text);
            PlayerPrefs.Save();
        }
    }
}