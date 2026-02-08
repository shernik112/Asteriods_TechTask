using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : ManagedBehaviour
    {
        private BulletPool _bulletPool;
        private Rigidbody2D _rg;
    
        [field: SerializeField] public float MoveSpeed { get; private set; } = default;

        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
    
        private void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
        }

        protected override void ManagedFixedUpdate()
        { 
            _rg.linearVelocity = transform.up * MoveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{typeof(Bullet)} bullet hit enemy");
            _bulletPool.ReturnToPool(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{typeof(Bullet)} bullet entered trigger");
            _bulletPool.ReturnToPool(gameObject);
        }
    }
}
