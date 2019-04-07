using UnityEngine;
using UnityEngine.UI;

namespace QuantumTek.QMenus
{
    /// <summary> Represents saves and loads a toggle setting. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Save Toggle")]
    [DisallowMultipleComponent]
    public class QMSaveToggle : MonoBehaviour
    {
        public string settingName;
        public Toggle reference;

        protected void Awake()
        {
            if (!reference || settingName.Length == 0) return;
            reference.isOn = PlayerPrefs.GetInt(settingName) == 1;
        }

        public void Save()
        {
            if (!reference || settingName.Length == 0) return;
            PlayerPrefs.SetInt(settingName, reference.isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}