using UnityEngine;
using UnityEngine.UI;

namespace QuantumTek.QMenus
{
    /// <summary> How the tab is animated when it is opened/closed. </summary>
    [System.Serializable]
    public enum QMTAnimationType
    {
        /// <summary> The sprite of the tab is set. </summary>
        Sprite,
        /// <summary> The color of the tab is set. </summary>
        Color,
        /// <summary> A boolean of the tab's animator is set. </summary>
        AnimatorBool,
        /// <summary> A trigger of the tab's animator is set. </summary>
        AnimatorTrigger,
        /// <summary> An integer of the tab's animator is set. </summary>
        AnimatorInt,
        /// <summary> A float of the tab's animator is set. </summary>
        AnimatorFloat
    }

    /// <summary> Represents a UI tab, used on a tab window. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Tab")]
    [DisallowMultipleComponent]
    public class QMTab : MonoBehaviour
    {
        public Image image;
        public Animator animator;
        public QMTAnimationType animationType;
        [HideInInspector] public Sprite openSprite;
        [HideInInspector] public Sprite closeSprite;
        [HideInInspector] public Color openColor;
        [HideInInspector] public Color closeColor;
        [HideInInspector] public string animatorOpen;
        [HideInInspector] public string animatorClose;
        [HideInInspector] public string animatorParameter;
        [HideInInspector] public string animatorOpenTrigger;
        [HideInInspector] public string animatorCloseTrigger;
        [HideInInspector] public int animatorOpenInt;
        [HideInInspector] public int animatorCloseInt;
        [HideInInspector] public float animatorOpenFloat;
        [HideInInspector] public float animatorCloseFloat;
        public bool isOpen;

        /// <summary> Changes the tab group's open state. </summary>
        /// <param name="pOpen">Whether or not the tab group is open.</param>
        public void ChangeOpenState(bool pOpen)
        {
            if (isOpen == pOpen) return;
            isOpen = pOpen;
            if (animationType == QMTAnimationType.Sprite && image && openSprite && closeSprite) image.sprite = isOpen ? openSprite : closeSprite;
            else if (animationType == QMTAnimationType.Color && image) image.color = isOpen ? openColor : closeColor;
            else if (animationType == QMTAnimationType.AnimatorBool && animator) animator.SetBool(animatorParameter, isOpen);
            else if (animationType == QMTAnimationType.AnimatorTrigger && animator) animator.SetTrigger(isOpen ? animatorOpenTrigger : animatorCloseTrigger);
            else if (animationType == QMTAnimationType.AnimatorInt && animator) animator.SetInteger(animatorParameter, isOpen ? animatorOpenInt : animatorCloseInt);
            else if (animationType == QMTAnimationType.AnimatorFloat && animator) animator.SetFloat(animatorParameter, isOpen ? animatorOpenFloat : animatorCloseFloat);
        }

        public void OverrideOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (animationType == QMTAnimationType.Sprite && image && openSprite && closeSprite) image.sprite = isOpen ? openSprite : closeSprite;
            else if (animationType == QMTAnimationType.Color && image) image.color = isOpen ? openColor : closeColor;
            else if (animationType == QMTAnimationType.AnimatorBool && animator) animator.SetBool(animatorParameter, isOpen);
            else if (animationType == QMTAnimationType.AnimatorTrigger && animator) animator.SetTrigger(isOpen ? animatorOpenTrigger : animatorCloseTrigger);
            else if (animationType == QMTAnimationType.AnimatorInt && animator) animator.SetInteger(animatorParameter, isOpen ? animatorOpenInt : animatorCloseInt);
            else if (animationType == QMTAnimationType.AnimatorFloat && animator) animator.SetFloat(animatorParameter, isOpen ? animatorOpenFloat : animatorCloseFloat);
            if (animator && animatorOpen.Length > 0 && animatorClose.Length > 0) animator.PlayInFixedTime(isOpen ? animatorOpen : animatorClose, 0, 1);
        }

        public void InspectorChangeOpenState(bool pOpen)
        {
            isOpen = pOpen;
            if (image && openSprite && closeSprite) image.sprite = isOpen ? openSprite : closeSprite;
            else if (image) image.color = isOpen ? openColor : closeColor;
        }
    }
}