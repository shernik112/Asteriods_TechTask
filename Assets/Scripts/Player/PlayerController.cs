using Project.System;
using Project.Enemies;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, ITeleported
    {
        [SerializeField] private PlayerData _playerData;

        private EventBus _eventBus;
        private MainAudio _mainAudio;
        private PauseHandler _pauseHandler;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _input;
    
        public ShootLaser Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }
        public Rigidbody2D Rb { get; private set; }

        [Inject]
        public void Construct(
            EventBus eventBus,
            PauseHandler pauseHandler,
            MainAudio mainAudio)
        {
            _eventBus = eventBus;
            _pauseHandler = pauseHandler;
            _mainAudio = mainAudio;
        }
    
        private void Awake()
        {
            Laser = GetComponentInChildren<ShootLaser>(true);
            BulletShoot = GetComponent<BulletShoot>();
            Rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _eventBus.OnRestartGame += SetActive;
        }
    
        private void OnDestroy() => _eventBus.OnRestartGame -= SetActive;

        public void SetInput(Vector2 input)
        {
            _input = input;
            _input.Normalize();
        }

        public bool TeleportReaction()
        {
            _mainAudio.PlaySfx(_playerData.dashClip);
            return true;
        }

        private void FixedUpdate()
        {
            if (_pauseHandler.IsPause)
                return;
            Rb.angularVelocity = Mathf.MoveTowards(Rb.angularVelocity, -_input.x * _playerData.rotateSpeed, _playerData.rotateAcceleration * Time.fixedDeltaTime);
            Vector2 targetVelocity = transform.up * _input.y * _playerData.moveSpeed;
            Rb.linearVelocity = Vector2.MoveTowards(Rb.linearVelocity, targetVelocity, _playerData.moveAcceleration * Time.fixedDeltaTime);
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IEnemy>(out var enemy))
            {
                Rb.linearVelocity = Vector2.zero;
                Rb.angularVelocity = default;
                _mainAudio.PlaySfx(_playerData.destructionClip);
                _eventBus.OnHitPlayer?.Invoke();
                SetDefaultValues();
            } 
            Debug.Log($"{typeof(PlayerController)} OnCollisionEnter");
        }

        private void SetDefaultValues()
        {
            _spriteRenderer.enabled = false;
            Rb.linearVelocity = Vector2.zero;
            transform.position = new Vector2(0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void SetActive() => _spriteRenderer.enabled = true;
    }
}
