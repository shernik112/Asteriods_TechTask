using IPoolable = Project.System.IPoolable;
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

    public abstract class BaseEnemy : MonoBehaviour, IEnemy, IPoolable, ITeleportedReaction
    {
        public event Action<GameObject> OnDeactivation;

        [SerializeField] protected EnemyDefinition enemyData = default;
        
        protected SpriteRenderer SpriteRenderer;
        protected Rigidbody2D Rb;
        
        private readonly WaitForSeconds _timeHitReaction = new WaitForSeconds(0.08f);
       
        private EventBus _eventBus;
        private HandlerScore _handlerScore;
        private MainAudio _mainAudio;
        
        public bool IsFirstEnterToTeleport { get; set; } = false;
        
        protected PlayerController PlayerController { get; private set; }
        
        [Inject]
        public void Construct(
            PlayerController playerController,
            EventBus eventBus, 
            HandlerScore handlerScore,
            MainAudio mainAudio)
        {
            PlayerController = playerController;
            _eventBus = eventBus;
            _handlerScore = handlerScore;
            _mainAudio = mainAudio;
        }

        protected virtual void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = enemyData.sprite;
            Rb = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable() =>
            _eventBus.OnHitPlayer += Deactivation;

        private void OnDisable() =>
            _eventBus.OnHitPlayer -= Deactivation;
        
        public virtual void OnReturnToPool(){}

        public void TeleportReaction(){}

        protected virtual void HitBullet() => Deactivation();

        private void HitLaser() => Deactivation();

        protected void Deactivation() =>
            OnDeactivation?.Invoke(gameObject);
        
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
            SpriteRenderer.sprite = enemyData.hitSprite;
            _mainAudio.PlaySfx(enemyData.hitClip);
            yield return _timeHitReaction;            

            SpriteRenderer.sprite = enemyData.sprite;
            _handlerScore.CountScoreDefeatedEnemy(enemyData.scoreByHit);
            typeHit?.Invoke();
        }
    }
}