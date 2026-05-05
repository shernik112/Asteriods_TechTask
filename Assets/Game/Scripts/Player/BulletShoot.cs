using Random = UnityEngine.Random;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class BulletShoot : MonoBehaviour
    {
        [SerializeField] private AudioClip bulletShotClip;
        [SerializeField] private float instantiateOffset;
        [SerializeField] private float cooldown;
        
        private ObjectPool _bulletPool;
        private AudioHandler _audioHandler;
        private float _defaultCooldown;
        private float _lastTime;
        private bool _mayShoot;

        [Inject]
        public void Construct(
            AudioHandler audioHandler, 
            ObjectPool bulletPool)
        {
            _audioHandler = audioHandler;
            _bulletPool = bulletPool;
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
                _audioHandler.PlaySfx(bulletShotClip);
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
