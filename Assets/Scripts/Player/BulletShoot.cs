
using Project.System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Player
{
    public class BulletShoot : MonoBehaviour
    {
        [SerializeField] private AudioClip bulletShotClip = default;
        [SerializeField] private float cooldown = default;
        [SerializeField] private float instantiateOffset = default;
    
        private BulletPool _bulletPool;
        private MainAudio _mainAudio;
        private float _defaultCooldown;
        private float _lastTime;
        private bool _mayShoot;

        [Inject]
        public void Construct(
            BulletPool bulletPool,
            MainAudio mainAudio)
        {
            _bulletPool = bulletPool;
            _mainAudio = mainAudio;
        }

        private void Awake()
        {
            _defaultCooldown = cooldown;
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
            var obj = _bulletPool.Get();
            var spawnPos = (Vector2)transform.position + (Vector2)transform.up * instantiateOffset;
            obj.transform.position = spawnPos;
            obj.transform.rotation = transform.localRotation;
        }
    }
}
