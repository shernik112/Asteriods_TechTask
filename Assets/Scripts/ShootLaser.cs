using System;
using UnityEngine;
using Zenject;
using Pixelplacement;

public class ShootLaser : ManagedBehaviour
{
    public int CurrentCountShоtLaser
    {
        get => _currentCountShotLaser;
        private set
        {
            _currentCountShotLaser = value;
            _countLaserShots.UpdateValue(_currentCountShotLaser);

        }
    }
    
    [Inject] private CountLaserShots _countLaserShots;
    [Inject] private RestartInvoke _restartInvoke;
    public readonly float RechargeDuration = 12;
    private readonly int _defaultCountShotLaser = 3;
    private int _currentCountShotLaser = default;
    private float _durationLaserShot = 0.4f;
    private float _cooldownDuration = 0.5f;
    private Quaternion _initialLaserRotation;
    [HideInInspector] public float lastRechargeTime;
    private float _lastShootTime;
    private Transform _parentTransform;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    private void OnEnable()
    {
        _restartInvoke.OnRestartGame += Restart;
    }

    private void OnDisable()
    {
        _restartInvoke.OnRestartGame -= Restart;
    }

    private void Awake()
    {
        _parentTransform = transform.parent;
        _initialLaserRotation = _parentTransform.localRotation;
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        TryGetComponent<Collider2D>(out _collider2D);
    }

    public void Start()
    {
        CurrentCountShоtLaser = _defaultCountShotLaser;
    }

    protected override void OnUpdate()
    {
        lastRechargeTime += Time.deltaTime;
        _lastShootTime += Time.deltaTime;
        
        if (lastRechargeTime >= RechargeDuration && CurrentCountShоtLaser < _defaultCountShotLaser)
        {
            lastRechargeTime = 0f;
            IncreaseCountShotLaser();
        }
    }

    public void TryShoot()
    {
        if (_lastShootTime >= _cooldownDuration && CurrentCountShоtLaser > 0)
        {
            Debug.Log($"{typeof(ShootLaser)} ShootLaser");
            Shoot();
            CurrentCountShоtLaser -= 1;
            _countLaserShots.UpdateValue(CurrentCountShоtLaser);
            _lastShootTime = 0f;
        }
    }
    private void Shoot()
    {
        ChangeVisibility(true);
        TurnLaser();
    }
    private void Restart()
    {
        Debug.Log($"{typeof(ShootLaser)} restart event");
        CurrentCountShоtLaser = _defaultCountShotLaser;
        lastRechargeTime = default;
    }
    private void IncreaseCountShotLaser()
    {
        CurrentCountShоtLaser += 1;
    }
    private void TurnLaser()
    {
        Tween.Rotate(_parentTransform, new Vector3(0, 0, -180),Space.Self, _durationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
            () =>
            {
                ChangeVisibility(false);
                _parentTransform.localRotation = _initialLaserRotation;
            });
    }

    private void ChangeVisibility(bool visibility)
    {
        _spriteRenderer.enabled = visibility;
        _collider2D.enabled = visibility;
    }
}
