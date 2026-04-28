using System;
using Pixelplacement;
using Project.System;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
    public class ShootLaser : MonoBehaviour
    {
        public event Action<int> NewCountShotLaser;
        [SerializeField] private LaserData laserData = default;
    
        private int _currentCountShotLaser = default;
        private float _lastShootTime;
        private RestartButton _restartButton;
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
                NewCountShotLaser?.Invoke(_currentCountShotLaser);
            }
        }

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
            _initialLaserRotation = transform.localRotation;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            ChangeVisibility(false);
                _restartButton.OnRestartGame += Restart;
            CurrentCountShоtLaser = laserData.DefaultCountShotLaser;
        }
        
        private void OnDestroy()
        {
            _restartButton.OnRestartGame -= Restart;
        }

        private void Update()
        {
            LastRechargeTime += Time.deltaTime;
            _lastShootTime += Time.deltaTime;
        
            if (LastRechargeTime >= RechargeDuration && CurrentCountShоtLaser < laserData.DefaultCountShotLaser)
            {
                LastRechargeTime = 0f;
                IncreaseCountShotLaser();
            }
        }
        
        public void TryShoot()
        {
            if (_lastShootTime >= laserData.CooldownDuration && CurrentCountShоtLaser > 0)
            {
                Shoot();
                CurrentCountShоtLaser -= 1;
                NewCountShotLaser?.Invoke(CurrentCountShоtLaser);
                _lastShootTime = 0f;
            }
        }
    
        private void Shoot()
        {
            _mainAudio.PlaySfx(laserData.ShootLaser);
            ChangeVisibility(true);
            TurnLaser();
        }
    
        private void Restart()
        {
            CurrentCountShоtLaser = laserData.DefaultCountShotLaser;
            LastRechargeTime = default;
        }
    
        private void IncreaseCountShotLaser()
        {
            CurrentCountShоtLaser += 1;
            _mainAudio.PlaySfx(laserData.NewChargeClip);
        }
    
        private void TurnLaser()
        {
            Tween.Rotate(transform, new Vector3(0, 0, -180),Space.Self, laserData.DurationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
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
