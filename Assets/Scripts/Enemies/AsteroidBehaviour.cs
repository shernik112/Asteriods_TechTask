using Quaternion = UnityEngine.Quaternion;
using System;
using UnityEngine;

namespace Project.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidBehaviour : BaseEnemy
    {
        public event Action<Transform> OnHitAsteroid;
        
        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Rb.linearVelocity = transform.right * enemyData.speed;
        }

        private void LateUpdate()
        {
            SpriteRenderer.transform.localRotation = Quaternion.Inverse(transform.rotation);
        }

        public override void ReturnToPool()
        {
            Rb.linearVelocity = Vector2.zero;
            Rb.angularVelocity = 0f;
            transform.localRotation = Quaternion.identity;
            OnHitAsteroid = null;
            SpriteRenderer.sprite = enemyData.sprite;
        }
        
        protected override void HitBullet()
        {
            OnHitAsteroid?.Invoke(transform);
            Deactivation();
        }
    }
}
