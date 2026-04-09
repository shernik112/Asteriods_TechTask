using System;
using Project.System;
using Project.Enemies;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, ITeleportedReaction
    {
        public event Action OnHitPlayer;
            
        [SerializeField] private PlayerData data;

        private RestartButton _restartButton;
        private MainAudio _mainAudio;
        private PauseHandler _pauseHandler;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _input;

        public Rigidbody2D Rb { get; private set; }
        public ShootLaser Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }

        
        [Inject]
        public void Construct(
            RestartButton restartButton,
            PauseHandler pauseHandler,
            MainAudio mainAudio)
        {
            _restartButton = restartButton;
            _pauseHandler = pauseHandler;
            _mainAudio = mainAudio;
        }

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            BulletShoot = GetComponent<BulletShoot>();
            Laser = GetComponentInChildren<ShootLaser>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _restartButton.OnRestartGame += SetActive;
        }

        private void OnDestroy() =>
            _restartButton.OnRestartGame -= SetActive;

        public void SetInput(Vector2 input)
        {
            _input = input.normalized;
        }

        public void TeleportReaction()
        {
            _mainAudio.PlaySfx(data.dashClip);
        }

        private void FixedUpdate()
        {
            if (_pauseHandler.IsPause)
                return;

            Rb.angularVelocity = Mathf.MoveTowards(
                Rb.angularVelocity,
                -_input.x * data.rotateSpeed,
                data.rotateAcceleration * Time.fixedDeltaTime);

            Vector2 targetVelocity = transform.up * _input.y * data.moveSpeed;

            Rb.linearVelocity = Vector2.MoveTowards(
                Rb.linearVelocity,
                targetVelocity,
                data.moveAcceleration * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IEnemy>(out _))
            {
                Rb.linearVelocity = Vector2.zero;
                Rb.angularVelocity = 0f;

                _mainAudio.PlaySfx(data.destructionClip);
                OnHitPlayer?.Invoke();

                SetDefaultValues();
            }

            Debug.Log($"{typeof(PlayerController)} OnCollisionEnter");
        }

        private void SetDefaultValues()
        {
            _spriteRenderer.enabled = false;
            Rb.linearVelocity = Vector2.zero;
            transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        }

        private void SetActive() =>
            _spriteRenderer.enabled = true;
    }
}