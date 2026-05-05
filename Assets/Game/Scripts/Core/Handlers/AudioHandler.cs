using UnityEngine;

namespace Project.System
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip mainTrack;
        [SerializeField] private float volumeSfx;
        
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
            _audioSource.PlayOneShot(audioClip, Mathf.Clamp01(volumeSfx));
        }
    }
}
