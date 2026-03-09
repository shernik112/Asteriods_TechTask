using Pixelplacement;
using Project.System;
using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
    public class ShootLaser : MonoBehaviour
    {
        [SerializeField] private LaserData laserData = default;
        
        private readonly int _defaultCountShotLaser = 3;
        private readonly float _durationLaserShot = 0.4f;
        private readonly float _cooldownDuration = 0.5f;
    
        private int _currentCountShotLaser = default;
        private float _lastShootTime;
        private EventBus _eventBus;
        private Quaternion _initialLaserRotation;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;
        private MainAudio _mainAudio;
    
        public float LastRechargeTime { get; private set; }
        public float RechargeDuration => 12;

        public int CurrentCountShоtLaser
        {
            get => _currentCountShotLaser;
            private set
            {
                _currentCountShotLaser = value;
                _eventBus.NewCountShotLaser?.Invoke(_currentCountShotLaser);
            }
        }
        
        private void Awake()
        {
            _initialLaserRotation = transform.localRotation;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            ChangeVisibility(false);
            _eventBus.OnRestartGame += Restart;
            CurrentCountShоtLaser = _defaultCountShotLaser;
        }
        
        private void OnDestroy()
        {
            _eventBus.OnRestartGame -= Restart;
        }

        private void Update()
        {
            LastRechargeTime += Time.deltaTime;
            _lastShootTime += Time.deltaTime;
        
            if (LastRechargeTime >= RechargeDuration && CurrentCountShоtLaser < _defaultCountShotLaser)
            {
                LastRechargeTime = 0f;
                IncreaseCountShotLaser();
            }
        }

        public void Init( 
            EventBus eventBus,
            MainAudio mainAudio)
        {
            _eventBus = eventBus;
            _mainAudio = mainAudio;
        }
        
        public void TryShoot()
        {
            if (_lastShootTime >= _cooldownDuration && CurrentCountShоtLaser > 0)
            {
                Shoot();
                CurrentCountShоtLaser -= 1;
                _eventBus.NewCountShotLaser?.Invoke(CurrentCountShоtLaser);
                _lastShootTime = 0f;
            }
        }
    
        private void Shoot()
        {
            _mainAudio.PlaySfx(laserData.shootLaser);
            ChangeVisibility(true);
            TurnLaser();
        }
    
        private void Restart()
        {
            CurrentCountShоtLaser = _defaultCountShotLaser;
            LastRechargeTime = default;
        }
    
        private void IncreaseCountShotLaser()
        {
            CurrentCountShоtLaser += 1;
            _mainAudio.PlaySfx(laserData.newChargeClip);
        }
    
        private void TurnLaser()
        {
            Tween.Rotate(transform, new Vector3(0, 0, -180),Space.Self, _durationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
                () =>
                {
                    ChangeVisibility(false);
                    transform.localRotation = _initialLaserRotation;
                });
        }

        private void ChangeVisibility(bool visibility)
        {
            _spriteRenderer.enabled = visibility;
            _collider2D.enabled = visibility;
        }
    }
}
