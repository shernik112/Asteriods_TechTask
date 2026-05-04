using System;
using Project.Enemies;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerDeathHandler : MonoBehaviour
    {
        public event Action OnHitPlayer;
        
        private PlayerData _data;
        private MainAudio _mainAudio;
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(PlayerData data, MainAudio mainAudio)
        {
            _data = data;
            _mainAudio = mainAudio;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<IEnemy>(out _))
                return;

            Die();
        }

        private void Die()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;

            _mainAudio.PlaySfx(_data.DestructionClip);
            OnHitPlayer?.Invoke();

            _spriteRenderer.enabled = false;
            transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        }

        public void ResetState()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _spriteRenderer.enabled = true;
            transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        }
    }
}