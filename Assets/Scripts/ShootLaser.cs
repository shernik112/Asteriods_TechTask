using UnityEngine;
using Zenject;
using Pixelplacement;

[RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
public class ShootLaser : ManagedBehaviour
{
    
    public readonly float RechargeDuration = 12;
    private readonly int _defaultCountShotLaser = 3;
    
    private int _currentCountShotLaser = default;
    private float _durationLaserShot = 0.4f;
    private float _cooldownDuration = 0.5f;
    private float _lastShootTime;
    private CountLaserShots _countLaserShots;
    private RestartInvoke _restartInvoke;
    private Quaternion _initialLaserRotation;
    private Transform _parentTransform;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    
    public float LastRechargeTime { get; private set; }
    public int CurrentCountShоtLaser
    {
        get => _currentCountShotLaser;
        private set
        {
            _currentCountShotLaser = value;
            _countLaserShots.UpdateValue(_currentCountShotLaser);

        }
    }

    [Inject]
    public void Construct(
        CountLaserShots countLaserShots,
        RestartInvoke restartInvoke)
    {
        _countLaserShots = countLaserShots;
        _restartInvoke = restartInvoke;
    }
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void Start()
    {
        CurrentCountShоtLaser = _defaultCountShotLaser;
    }

    protected override void OnUpdate()
    {
        LastRechargeTime += Time.deltaTime;
        _lastShootTime += Time.deltaTime;
        
        if (LastRechargeTime >= RechargeDuration && CurrentCountShоtLaser < _defaultCountShotLaser)
        {
            LastRechargeTime = 0f;
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
        LastRechargeTime = default;
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
