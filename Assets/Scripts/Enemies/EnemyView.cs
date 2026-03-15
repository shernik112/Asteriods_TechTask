using System.Collections;
using Project.Player;
using Project.System;
using UnityEngine;
using System;

namespace Project.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyView<TModel> : MonoBehaviour, ITeleportedReaction, IEnemy
        where TModel : EnemyModel
    {
        public event Action<EnemyTypeHit> OnHitReaction;
        
        protected TModel Model;
        protected Rigidbody2D Rb;
        protected SpriteRenderer SpriteRenderer;
        
        public bool IsFirstEnterToTeleport { get; set; } = false;
        
        private readonly WaitForSeconds _timeHitReaction = new WaitForSeconds(0.08f);

        public void Init(
            TModel model)
        {
            Model = model;
            
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = Model.Data.sprite;
            Rb = GetComponent<Rigidbody2D>();
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out var playerBullet))
                OnHitReaction?.Invoke(EnemyTypeHit.Bullet);
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<LaserController>(out var laser))
                OnHitReaction?.Invoke(EnemyTypeHit.Laser);
        }
        public void TeleportReaction(){}
        
        public IEnumerator PlayHitReaction()
        {
            SpriteRenderer.sprite = Model.Data.hitSprite;
            yield return _timeHitReaction;            

            SpriteRenderer.sprite = Model.Data.sprite;
        }

        public abstract void SetDefaultValues();
    }
}
