using System;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private ObjectPool _bulletPool;
        private PlayerController _playerController;
        private Rigidbody2D _rg;
    
        [field: SerializeField] public float MoveSpeed { get; private set; } = default;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
            _rg = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _bulletPool = _playerController.BulletShoot.BulletPool;
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
    }
}
