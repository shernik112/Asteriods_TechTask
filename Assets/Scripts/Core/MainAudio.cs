using UnityEngine;

namespace Project.System
{
    [RequireComponent(typeof(AudioSource))]
    public class MainAudio : MonoBehaviour
    {
        private float _volumeSfx = 0.75f;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySfx(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip, Mathf.Clamp01(_volumeSfx));
        }
    }
}
