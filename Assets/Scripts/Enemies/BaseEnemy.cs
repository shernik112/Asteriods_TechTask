using System;
using System.Collections;
using Project.Player;
using Project.System;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Enemies
{
    public interface IEnemy
    {
        bool IsFirstEnterToTeleport { get ; set; }
    }

    public abstract class BaseEnemy<TPool> : MonoBehaviour, IEnemy
        where TPool : ObjectPool
    {
        protected Sprite HitSprite;
        protected TPool Pool;
        
        private readonly WaitForSeconds _timeHitReaction = new WaitForSeconds(0.08f);
        private HandlerScore _handlerScore;
        
        protected PlayerController PlayerController { get; private set; }
        protected SpriteRenderer SpriteRenderer;
        public bool IsFirstEnterToTeleport { get; set; } = false;
        public abstract int CountScoreByDefeat { get; set; }
    
        [Inject]
        public void Construct(TPool pool, PlayerController playerController, HandlerScore handlerScore)
        {
            Pool = pool;
            PlayerController = playerController;
            _handlerScore = handlerScore;
        }

        protected virtual void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            if (PlayerController != null) PlayerController.OnHitPlayer += ReturnSelf;
        }

        private void OnDisable()
        {
            if (PlayerController != null) PlayerController.OnHitPlayer -= ReturnSelf;
        }

        protected virtual void HitBullet() => Pool.ReturnToPool(gameObject);
    
        protected virtual void HitLaser() => Pool.ReturnToPool(gameObject);

        private void ReturnSelf()
        {
            Debug.Log($"{typeof(BaseEnemy<>)} ReturnSelf ");
            Pool.ReturnToPool(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out var playerBullet))
                StartCoroutine(HitBulletReaction());
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ShootLaser>(out var laser))
                StartCoroutine(HitLaserReaction());
            
            Debug.Log($"{typeof(BaseEnemy<>)} Laser Trigger");
        }
        
        private IEnumerator HitLaserReaction()
        {
            var defaultSprite = SpriteRenderer.sprite;
            if (HitSprite != null) SpriteRenderer.sprite = HitSprite;
            yield return _timeHitReaction;
            SpriteRenderer.sprite = defaultSprite;
            _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
            HitLaser();
        }

        private IEnumerator HitBulletReaction()
        {
            var defaultSprite = SpriteRenderer.sprite;
            if (HitSprite != null)
                SpriteRenderer.sprite = HitSprite;
            yield return _timeHitReaction;
            SpriteRenderer.sprite = defaultSprite;
            _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
            HitBullet();
        }
    }
}