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

    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private AudioClip hitClip = default;
        
        protected Sprite HitSprite;
        protected ObjectPool Pool;
        protected Rigidbody2D Rb;
        protected EnemiesSpawner EnemiesSpawner;
        
        private readonly WaitForSeconds _timeHitReaction = new WaitForSeconds(0.08f);
        private HandlerScore _handlerScore;
        private MainAudio _mainAudio;
        
        protected PlayerController PlayerController { get; private set; }
        protected SpriteRenderer SpriteRenderer;
        public bool IsFirstEnterToTeleport { get; set; } = false;
        public abstract int CountScoreByDefeat { get; set; }
    
        [Inject]
        public void Construct(
            PlayerController playerController, 
            EnemiesSpawner enemiesSpawner,
            HandlerScore handlerScore,
            MainAudio mainAudio)
        {
            PlayerController = playerController;
            EnemiesSpawner = enemiesSpawner;
            _handlerScore = handlerScore;
            _mainAudio = mainAudio;
            
            Initialize();
        }

        protected virtual void Initialize()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            if (PlayerController != null) 
                PlayerController.OnHitPlayer += ReturnSelf;
        }

        private void OnDisable()
        {
            if (PlayerController != null) 
                PlayerController.OnHitPlayer -= ReturnSelf;
        }

        protected virtual void HitBullet() => Pool.ReturnToPool(gameObject);
    
        private void HitLaser() => Pool.ReturnToPool(gameObject);

        private void ReturnSelf()
        {
            Debug.Log($"{typeof(BaseEnemy)} ReturnSelf ");
            Pool.ReturnToPool(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out var playerBullet))
                StartCoroutine(PlayHitReaction(HitBullet));
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ShootLaser>(out var laser))
                StartCoroutine(PlayHitReaction(HitLaser));
        }
        
        private IEnumerator PlayHitReaction(Action typeHit)
        {
            var defaultSprite = SpriteRenderer.sprite;
            
            if (HitSprite != null)
                SpriteRenderer.sprite = HitSprite;
            _mainAudio.PlaySfx(hitClip);
            yield return _timeHitReaction;            

            SpriteRenderer.sprite = defaultSprite;
            _handlerScore.CountScoreDefeatedEnemy(CountScoreByDefeat);
            typeHit?.Invoke();
        }
    }
}