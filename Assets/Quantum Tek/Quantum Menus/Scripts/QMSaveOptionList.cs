using UnityEngine;

namespace QuantumTek.QMenus
{
    /// <summary> Represents saves and loads an option list setting. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Save Option List")]
    [DisallowMultipleComponent]
    public class QMSaveOptionList : MonoBehaviour
    {
        public string settingName;
        public QMOptionList reference;

        protected void Awake()
        {
            if (!reference || settingName.Length == 0) return;
            reference.SetOption(PlayerPrefs.GetInt(settingName));
        }

        public void Save()
        {
            if (!reference || settingName.Length == 0) return;
            PlayerPrefs.SetInt(settingName, reference.currentOption);
            PlayerPrefs.Save();
        }
    }
}