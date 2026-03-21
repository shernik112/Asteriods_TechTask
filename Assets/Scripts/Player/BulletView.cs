using System;
using Project.Player;
using UnityEngine;

namespace Project
{
    public class BulletView : View<BulletData>
    {
        public event Action OnHitReaction;

        private Rigidbody2D _rg;

        private void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        { 
            _rg.linearVelocity = transform.up * Model.Data.moveSpeed;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{typeof(BulletView)} bullet hit enemy");
            OnHitReaction?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{typeof(BulletView)} bullet entered trigger");
            OnHitReaction?.Invoke();
        }
    }
}
