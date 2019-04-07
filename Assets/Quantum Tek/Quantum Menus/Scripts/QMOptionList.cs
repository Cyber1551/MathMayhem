using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace QuantumTek.QMenus
{
    /// <summary> Represents a list of options to choose from. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Option List")]
    [DisallowMultipleComponent]
    public class QMOptionList : MonoBehaviour
    {
        public List<string> options;
        public TextMeshProUGUI optionText;
        public int currentOption;
        public UnityEvent onOptionChange;

        /// <summary> Returns the currently selected option. </summary>
        /// <returns>The selected option.</returns>
        public string GetOption()
        { return options[currentOption]; }

        /// <summary> Gets the index of an option by text. </summary>
        /// <returns>The option's index.</returns>
        /// <param name="pOption">The option text.</param>
        public int GetOptionIndex(string pOption)
        {
            int count = options.Count;
            return options.IndexOf(pOption);
        }

        /// <summary> Changes the currently selected option by a certain amount. </summary>
        /// <param name="pAmount">The amount to change by.</param>
        public void ChangeOption(int pAmount)
        {
            currentOption += pAmount;
            if (currentOption >= options.Count) currentOption = 0;
            else if (currentOption < 0) currentOption = options.Count;
            SetOption(currentOption);
        }

        /// <summary> Sets the currently selected option to a certain option. </summary>
        /// <param name="pOption">The new selected option.</param>
        public void SetOption(int pOption)
        {
            currentOption = Mathf.Clamp(pOption, 0, options.Count);
            if (optionText && options.Count > 0 && currentOption < options.Count) optionText.text = options[currentOption];
            onOptionChange.Invoke();
        }
        /// <summary> Sets the currently selected option to a certain option. </summary>
        /// <param name="pOption">The text of the new selected option.</param>
        public void SetOption(string pOption)
        {
            currentOption = Mathf.Clamp(GetOptionIndex(pOption), 0, options.Count);
            if (optionText && options.Count > 0 && currentOption < options.Count) optionText.text = options[currentOption];
            onOptionChange.Invoke();
        }
    }
}