using UnityEngine;
using UnityEngine.UI;

namespace QuantumTek.QMenus
{
    /// <summary> The fill type of a UI bar. </summary>
    [System.Serializable]
    public enum QMFillType
    {
        /// <summary> Set the width of the fill image as a percent of the fill bar's width. </summary>
        Width,
        /// <summary> Set the height of the fill image as a percent of the fill bar's height. </summary>
        Height,
        /// <summary> Set fill amount of the fill image. </summary>
        FillAmount
    }

    /// <summary> Represents a UI bar, useful for XP, health, energy, or whatever else requires a bar. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Bar")]
    [DisallowMultipleComponent]
    public class QMBar : MonoBehaviour
    {
        public RectTransform bar;
        public RectTransform fill;
        public Image fillImage;
        public QMFillType type;

        /// <summary> Changes the percentage filled. </summary>
        /// <param name="pPercent">The percent represented as a number between 0 and 1.</param>
        public void ChangeFill(float pPercent)
        {
            if (!bar || !fill || !fillImage) return;
            pPercent = Mathf.Clamp(pPercent, 0, 1);
            
            if (type == QMFillType.Width) fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bar.rect.width * pPercent);
            else if (type == QMFillType.Height) fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, bar.rect.height * pPercent);
            else if (type == QMFillType.FillAmount) fillImage.fillAmount = pPercent;
        }
    }
}