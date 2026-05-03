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
        
        private int _currentCountShotLaser;
        private float _lastShootTime;
        private LaserData _laserData;
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
            LaserData laserData,
            RestartButton restartButton,
            MainAudio mainAudio)
        {
            _laserData = laserData;
            _restartButton = restartButton;
            _mainAudio = mainAudio;
        }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            
            CurrentCountShоtLaser = _laserData.DefaultCountShotLaser;
            _initialLaserRotation = transform.localRotation;
            
            _restartButton.OnRestartGame += Restart;
            ChangeVisibility(false);
        }
        
        private void OnDestroy()
        {
            _restartButton.OnRestartGame -= Restart;
        }

        private void Update()
        {
            LastRechargeTime += Time.deltaTime;
            _lastShootTime += Time.deltaTime;
        
            if (LastRechargeTime >= RechargeDuration && CurrentCountShоtLaser < _laserData.DefaultCountShotLaser)
            {
                LastRechargeTime = 0f;
                IncreaseCountShotLaser();
            }
        }
        
        public void TryShoot()
        {
            if (_lastShootTime >= _laserData.CooldownDuration && CurrentCountShоtLaser > 0)
            {
                Shoot();
                CurrentCountShоtLaser -= 1;
                NewCountShotLaser?.Invoke(CurrentCountShоtLaser);
                _lastShootTime = 0f;
            }
        }
    
        private void Shoot()
        {
            _mainAudio.PlaySfx(_laserData.ShootLaser);
            ChangeVisibility(true);
            TurnLaser();
        }
    
        private void Restart()
        {
            CurrentCountShоtLaser = _laserData.DefaultCountShotLaser;
            LastRechargeTime = default;
        }
    
        private void IncreaseCountShotLaser()
        {
            CurrentCountShоtLaser += 1;
            _mainAudio.PlaySfx(_laserData.NewChargeClip);
        }
    
        private void TurnLaser()
        {
            Tween.Rotate(transform, new Vector3(0, 0, -180),Space.Self, _laserData.DurationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
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
