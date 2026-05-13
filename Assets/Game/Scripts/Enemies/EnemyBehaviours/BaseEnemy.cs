using IPoolable = Project.System.IPoolable;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
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

        [SerializeField] protected EnemyDefinition enemyData;
        
        protected SpriteRenderer SpriteRenderer;
        protected Rigidbody2D Rb;
        
        private HandlerScore _handlerScore;
        private AudioHandler _audioHandler;
        private CancellationTokenSource _hitReactionCts;

        public bool IsFirstEnterToTeleport { get; set; } = false;
        
        protected PlayerController PlayerController { get; private set; }
        
        [Inject]
        public void Construct(
            PlayerController playerController,
            HandlerScore handlerScore,
            AudioHandler audioHandler)
        {
            PlayerController = playerController;
            _handlerScore = handlerScore;
            _audioHandler = audioHandler;
        }

        protected virtual void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = enemyData.Sprite;
            Rb = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            PlayerController.DeathHandler.OnHitPlayer += Deactivation;

            _hitReactionCts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            PlayerController.DeathHandler.OnHitPlayer -= Deactivation;

            _hitReactionCts?.Cancel();
            _hitReactionCts?.Dispose();
            _hitReactionCts = null;
        }
        
        public virtual void OnReturnToPool(){}

        public void TeleportReaction(){}

        protected virtual void HitBullet() => Deactivation();

        private void HitLaser() => Deactivation();

        protected void Deactivation() =>
            OnDeactivation?.Invoke(gameObject);
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out var _))
                PlayHitReaction(HitBullet, _hitReactionCts.Token).Forget();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ShootLaser>(out var _))
                PlayHitReaction(HitLaser, _hitReactionCts.Token).Forget();
        }

        private async UniTask PlayHitReaction(Action typeHit, CancellationToken cts)
        {
            SpriteRenderer.sprite = enemyData.HitSprite;
            _audioHandler.PlaySfx(enemyData.HitClip);
            await UniTask.Delay(enemyData.TimeHitReactionMs, cancellationToken: cts);

            SpriteRenderer.sprite = enemyData.Sprite;
            _handlerScore.CountScoreDefeatedEnemy(enemyData.ScoreByHit);
            typeHit?.Invoke();
        }
    }
}