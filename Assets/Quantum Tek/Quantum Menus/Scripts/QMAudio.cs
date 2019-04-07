using UnityEngine;

namespace QuantumTek.QMenus
{
    /// <summary> Represents a audio to be played when UI is interacted with. </summary>
    [AddComponentMenu("Quantum Tek/Menus/Audio")]
    [DisallowMultipleComponent]
    public class QMAudio : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip clip;

        /// <summary> Plays the default audio. </summary>
        public void PlayAudio()
        { if (source && clip) source.PlayOneShot(clip); }
        /// <summary> Plays the given audio. </summary>
        /// <param name="pClip">The clip to play.</param>
        public void PlayAudio(AudioClip pClip)
        { if (source && pClip) source.PlayOneShot(pClip); }
    }
}