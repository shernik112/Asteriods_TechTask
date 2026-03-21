using UnityEngine;

namespace Project.System
{
    [RequireComponent(typeof(AudioSource))]
    public class MainAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip mainTrack;
        private readonly float _volumeSfx = 0.75f;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.clip = mainTrack;
            _audioSource.Play();
        }

        public void PlaySfx(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip, Mathf.Clamp01(_volumeSfx));
        }
    }
}
