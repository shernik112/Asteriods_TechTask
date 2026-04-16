using Random = UnityEngine.Random;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class BulletShoot : MonoBehaviour
    {
        [SerializeField] private AudioClip bulletShotClip = default;
        
        private readonly float _instantiateOffset = 0.5f;
        private float _cooldown = 0.2f;
        private ObjectPool _bulletPool;
        private MainAudio _mainAudio;
        private float _defaultCooldown;
        private float _lastTime;
        private bool _mayShoot;

        [Inject]
        public void Construct(
            MainAudio mainAudio, 
            ObjectPool bulletPool)
        {
            _mainAudio = mainAudio;
            _bulletPool = bulletPool;
        }

        private void Awake()
        {
            _defaultCooldown = _cooldown;
        }

        private void Update()
        {
            _lastTime += Time.deltaTime;
        }

        public void TryShoot()
        {
            if (_lastTime >= _cooldown)
            {
                _mainAudio.PlaySfx(bulletShotClip);
                Shoot();
                _lastTime = 0f;
                _cooldown = _defaultCooldown + Random.Range(0.01f, 0.05f);
            }
        }
    
        private void Shoot()
        {
            var obj = _bulletPool.Get();
            var spawnPos = (Vector2)transform.position + (Vector2)transform.up * _instantiateOffset;
            obj.transform.position = spawnPos;
            obj.transform.rotation = transform.localRotation;
        }
    }
}
