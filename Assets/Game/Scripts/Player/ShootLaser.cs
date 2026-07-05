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
        [field:SerializeField] public float RechargeDuration { get; private set; }
        
        public event Action<int> NewCountShotLaser;
        public event Action OnLaserUsed;
        
        private int _currentCountShotLaser;
        private float _lastShootTime;
        private LaserData _laserData;
        private RestartButton _restartButton;
        private Quaternion _initialLaserRotation;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;
        private AudioHandler _audioHandler;
    
        public float LastRechargeTime { get; private set; }

        public int CurrentCountShotLaser
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
            AudioHandler audioHandler)
        {
            _laserData = laserData;
            _restartButton = restartButton;
            _audioHandler = audioHandler;
        }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            
            CurrentCountShotLaser = _laserData.DefaultCountShotLaser;
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
        
            if (LastRechargeTime >= RechargeDuration && CurrentCountShotLaser < _laserData.DefaultCountShotLaser)
            {
                LastRechargeTime = 0f;
                IncreaseCountShotLaser();
            }
        }
        
        public void TryShoot()
        {
            if (_lastShootTime >= _laserData.CooldownDuration && CurrentCountShotLaser > 0)
            {
                Shoot();
                CurrentCountShotLaser -= 1;
                NewCountShotLaser?.Invoke(CurrentCountShotLaser);
                _lastShootTime = 0f;
            }
        }
    
        private void Shoot()
        {
            _audioHandler.PlaySfx(_laserData.ShootLaser);
            ChangeVisibility(true);
            TurnLaser();
            OnLaserUsed?.Invoke();
        }
    
        private void Restart()
        {
            CurrentCountShotLaser = _laserData.DefaultCountShotLaser;
            LastRechargeTime = 0;
        }
    
        private void IncreaseCountShotLaser()
        {
            CurrentCountShotLaser += 1;
            _audioHandler.PlaySfx(_laserData.NewChargeClip);
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
