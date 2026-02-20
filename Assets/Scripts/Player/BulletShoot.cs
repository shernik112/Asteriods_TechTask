using Random = UnityEngine.Random;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class BulletShoot : MonoBehaviour
    {
        public ObjectPool BulletPool;
        
        [SerializeField] private AudioClip bulletShotClip = default;
        [SerializeField] private float instantiateOffset = default;
        [SerializeField] private float cooldown = default;
    
        private MainAudio _mainAudio;
        private MainInstaller _mainInstaller;
        private GameObject _bulletPrefab;
        private float _defaultCooldown;
        private float _lastTime;
        private bool _mayShoot;

        [Inject]
        public void Construct(
            MainAudio mainAudio,
            MainInstaller mainInstaller,
            GameObject bulletPrefab)
        {
            _mainAudio = mainAudio;
            _mainInstaller = mainInstaller;
            _bulletPrefab = bulletPrefab;
        }

        private void Awake()
        {
            _defaultCooldown = cooldown;
            BulletPool = new ObjectPool(_bulletPrefab, _mainInstaller, transform);
        }

        private void Update()
        {
            _lastTime += Time.deltaTime;
        }

        public void TryShoot()
        {
            if (_lastTime >= cooldown)
            {
                _mainAudio.PlaySfx(bulletShotClip);
                Shoot();
                _lastTime = 0f;
                cooldown = _defaultCooldown + Random.Range(0.01f, 0.05f);
            }
        }
    
        private void Shoot()
        {
            var obj = BulletPool.Get();
            var spawnPos = (Vector2)transform.position + (Vector2)transform.up * instantiateOffset;
            obj.transform.position = spawnPos;
            obj.transform.rotation = transform.localRotation;
        }
    }
}
