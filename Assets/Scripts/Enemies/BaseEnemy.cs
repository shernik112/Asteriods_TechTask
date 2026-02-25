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

    public abstract class BaseEnemy : MonoBehaviour, IEnemy, IPoolable, ITeleported
    {
        public event Action<GameObject> OnDeactivation;
        
        [SerializeField] private AudioClip hitClip = default;
        
        protected ObjectPool Pool;
        protected SpriteRenderer SpriteRenderer;
        protected Sprite HitSprite;
        protected Rigidbody2D Rb;
        
        private readonly WaitForSeconds _timeHitReaction = new WaitForSeconds(0.08f);
        private HandlerScore _handlerScore;
        private MainAudio _mainAudio;
        
        protected PlayerController PlayerController { get; private set; }
        
        public bool IsFirstEnterToTeleport { get; set; } = false;
        public abstract int CountScoreByDefeat { get; set; }
    
        [Inject]
        public void Construct(
            PlayerController playerController, 
            HandlerScore handlerScore,
            MainAudio mainAudio)
        {
            PlayerController = playerController;
            _handlerScore = handlerScore;
            _mainAudio = mainAudio;
        }

        protected virtual void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            if (PlayerController != null) 
                PlayerController.OnHitPlayer += Deactivation;
        }

        private void OnDisable()
        {
            if (PlayerController != null) 
                PlayerController.OnHitPlayer -= Deactivation;
        }

        public bool TeleportReaction()
        {
            if (IsFirstEnterToTeleport)
            {
                IsFirstEnterToTeleport = false;
                return false;
            }

            return true;
        }

        public void OnGetFromPool(ObjectPool pool) => Pool = pool;
        public virtual void OnReturnToPool(){}

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