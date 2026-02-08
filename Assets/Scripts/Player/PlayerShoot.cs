using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class PlayerShoot : ManagedBehaviour
    {
        [SerializeField] private float cooldown = default;
        [SerializeField] private float instantiateOffset = default;
    
        private BulletPool _bulletPool;
        private float _lastTime;
        private bool _mayShoot;

        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
        protected override void ManagedUpdate()
        {
            _lastTime += Time.deltaTime;
        }

        public void TryShoot()
        {
            if (_lastTime >= cooldown)
            {
                Shoot();
                _lastTime = 0f;
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
