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

    public abstract class BaseEnemy<TPool> : ManagedBehaviour, IEnemy
        where TPool : ObjectPool
    { 
        protected TPool Pool;
        private HandlerScore _handlerScore;
    
        protected PlayerController PlayerController { get; private set; }
        public bool IsFirstEnterToTeleport { get; set; } = false;
        public abstract int CountScoreByDefeat { get; set; }
    
        [Inject]
        public void Construct(TPool pool, PlayerController playerController, HandlerScore handlerScore)
        {
            Pool = pool;
            PlayerController = playerController;
            _handlerScore = handlerScore;
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
            {
                _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
                HitBullet();
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ShootLaser>(out var laser))
            {
                _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
                HitLaser();
            }
            Debug.Log($"{typeof(BaseEnemy<>)} Laser Trigger");
        }
    }
}