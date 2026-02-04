using System;
using UnityEngine;
using Zenject;
using Pixelplacement;

public class HandlerShootLaser : ManagedBehaviour
{
    public int CurrentCountShоtLaser => _currentCountShotLaser;
    [Inject] private CharacterController _chController;
    [Inject] private CountLaserShots _countLaserShots;
    [Inject] private RestartInvoke _restartInvoke;
    private readonly int _defaultCountShotLaser = 3;
    private int _currentCountShotLaser = default;
    private float _durationLaserShot = 0.4f;
    private GameObject _playerLaser;
    private float _rechargeDuration = 10f;
    private float _cooldownDuration = 0.5f;
    private Quaternion _initialLaserRotation;
    
    private float _lastRechargeTime;
    private float _lastShootTime;

    private void OnEnable()
    {
        _chController.OnHitPlayer += ResetValues;
        _restartInvoke.OnRestartGame += Restart;
    }

    private void OnDisable()
    {
        _chController.OnHitPlayer -= ResetValues;
        _restartInvoke.OnRestartGame -= Restart;
    }

    public override void ManagedInintialize()
    {
        _playerLaser = _chController.transform.GetChild(0)?.gameObject;
        _currentCountShotLaser = _defaultCountShotLaser;
        _initialLaserRotation = _playerLaser.transform.localRotation;
    }

    public void Start()
    {
        _countLaserShots.UpdateValue(_defaultCountShotLaser);
    }

    protected override void ManagedUpdate()
    {
        _lastRechargeTime += Time.deltaTime;
        _lastShootTime += Time.deltaTime;
        if (_lastRechargeTime >= _rechargeDuration && _currentCountShotLaser < _defaultCountShotLaser)
        {
            _lastRechargeTime = 0f;
            IncreaseCountShotLaser();
        }

        if (_lastShootTime >= _cooldownDuration && _currentCountShotLaser > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log($"{typeof(HandlerShootLaser)} ShootLaser");
                ShootLaser();
                _currentCountShotLaser -= 1;
                _countLaserShots.UpdateValue(_currentCountShotLaser);
                _lastShootTime = 0f;
            }
        }
    }

    private void ShootLaser()
    {
        _playerLaser.SetActive(true);
        TurnLaser();
    }

    private void ResetValues()
    {
        _currentCountShotLaser = _defaultCountShotLaser;
        _lastRechargeTime = default;
        
    }

    private void Restart() => _countLaserShots.UpdateValue(_defaultCountShotLaser);
    private void IncreaseCountShotLaser()
    {
        _currentCountShotLaser += 1;
        _countLaserShots.UpdateValue(_currentCountShotLaser);
    }
    private void TurnLaser()
    {
        Tween.Rotate(_playerLaser.transform, new Vector3(0, 0, -180),Space.Self, _durationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
            () =>
            {
                _playerLaser.SetActive(false);
                _playerLaser.transform.localPosition = Vector2.zero;
                _playerLaser.transform.localRotation = _initialLaserRotation;
            });
    }
}
