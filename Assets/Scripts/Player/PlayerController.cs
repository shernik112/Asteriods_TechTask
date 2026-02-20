using Project.System;
using Project.Enemies;
using UnityEngine;
using System;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    { 
        public event Action OnHitPlayer;

        [SerializeField] private AudioClip destructionClip = default;
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float speedAcceleration = default;
        [SerializeField] private float rotateSpeed = default;
        [SerializeField] private float rotateAcceleration = default;
        
        private RestartButton _restartButton;
        private MainAudio _mainAudio;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _input;
    
        public ShootLaser Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }
        public Rigidbody2D Rb { get; private set; }

        [Inject]
        public void Construct(
            RestartButton restartButton,
            MainAudio mainAudio)
        {
            _restartButton = restartButton;
            _mainAudio = mainAudio;
        }
    
        private void Awake()
        {
            Laser = GetComponentInChildren<ShootLaser>(true);
            BulletShoot = GetComponent<BulletShoot>();
            Rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _restartButton.OnRestartGame += SetActive;
        }
    
        private void OnDestroy() => _restartButton.OnRestartGame -= SetActive;

        public void SetInput(Vector2 input)
        {
            _input = input;
            _input.Normalize();
        }
    
        private void FixedUpdate()
        {
            Rb.angularVelocity = Mathf.MoveTowards(Rb.angularVelocity, -_input.x * rotateSpeed, rotateAcceleration * Time.fixedDeltaTime);
            Vector2 targetVelocity = transform.up * _input.y * moveSpeed;
            Rb.linearVelocity = Vector2.MoveTowards(Rb.linearVelocity, targetVelocity, speedAcceleration * Time.fixedDeltaTime);
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IEnemy>(out var enemy))
            {
                Debug.Log($"Invoke OnHitPlayer; subscribers = {(OnHitPlayer == null ? 0 : OnHitPlayer.GetInvocationList().Length)}");
                Rb.linearVelocity = Vector2.zero;
                Rb.angularVelocity = default;
                _mainAudio.PlaySfx(destructionClip);
                OnHitPlayer?.Invoke();
                SetDefaultValues();
            } 
            Debug.Log($"{typeof(PlayerController)} OnCollisionEnter");
        }

        private void SetDefaultValues()
        {
            _spriteRenderer.enabled = false;
            transform.position = new Vector2(0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void SetActive() => _spriteRenderer.enabled = true;
    }
}
