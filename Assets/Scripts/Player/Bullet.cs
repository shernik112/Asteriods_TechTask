using System;
using Project.System;
using UnityEngine;
using Zenject;
using IPoolable = Project.System.IPoolable;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        private ObjectPool _bulletPool;
        private Rigidbody2D _rg;
    
        [field: SerializeField] public float MoveSpeed { get; private set; } = default;

        private void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
        }

        private void Update()
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

        public void OnGetFromPool(ObjectPool objectPool)
        {
            _bulletPool = objectPool;
        }

        public void OnReturnToPool(){}
    }
}
