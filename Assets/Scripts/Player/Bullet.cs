using IPoolable = Project.System.IPoolable;
using UnityEngine;
using System;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        public event Action<GameObject> OnDeactivation;
        
        [SerializeField] private BulletData bulletData;
        
        private Rigidbody2D _rg;

        private void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bulletData.sprite;
        }

        private void Update()
        { 
            _rg.linearVelocity = transform.up * bulletData.moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{typeof(Bullet)} bullet hit enemy");
            OnDeactivation?.Invoke(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{typeof(Bullet)} bullet entered trigger");
            OnDeactivation?.Invoke(gameObject);
        }
        
        public void ReturnToPool(){}
    }
}
