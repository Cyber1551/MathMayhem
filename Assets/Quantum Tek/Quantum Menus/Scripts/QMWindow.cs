using UnityEngine;

namespace QuantumTek.QMenus
{
    /// <summary> How the window is animated when it is opened/closed. </summary>
    [System.Serializable]
    public enum QMWAnimationType
    {
        /// <summary> The window's content is activated/deactivated when the window is opened/closed. </summary>
        ActiveState,
        /// <summary> A boolean of the window's animator is set. </summary>
        AnimatorBool,
        /// <summary> A trigger of the window's animator is set. </summary>
        AnimatorTrigger,
        /// <summary> An integer of the window's animator is set. </summary>
        AnimatorInt,
        /// <summary> A float of the window's animator is set. </summary>
        AnimatorFloat
    }

    /// <summary> Represents a UI window, used in menus. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Window")]
    [DisallowMultipleComponent]
    public class QMWindow : MonoBehaviour
    {
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
            OverrideOpenState(isOpen);
        }

        /// <summary> Changes the window's open state. </summary>
        /// <param name="pOpen">Whether or not the window is open.</param>
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
    }
}