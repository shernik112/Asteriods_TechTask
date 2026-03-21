using System.Collections;
using Project.Core;
using Project.Player;
using Project.System;
using Project.UI;
using UnityEngine;
using Zenject;

public enum EnemyTypeHit
{
    Laser,
    Bullet
}

interface IEnemy
{
    bool IsFirstEnterToTeleport { get; set; }
}

namespace Project.Enemies
{
    public abstract class EnemyController<TModel> : PoolableController<TModel>
        where TModel : EnemyModel
    {
        [SerializeField] protected EnemyDefinition enemyData = default;
        
        protected EnemyView<TModel> View;
        protected TModel Model;
        
        private EventBus _eventBus;
        private MainAudio _mainAudio;
        private HandlerScore _handlerScore;
        
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
            Model = CreateModel();
            View = GetComponent<EnemyView<TModel>>();
            View.Init(Model);
        }

        private void OnEnable()
        {
            _eventBus.OnHitPlayer += Deactivation;
            View.OnHitReaction += StartHitReaction;
        }

        private void OnDisable()
        {
            _eventBus.OnHitPlayer -= Deactivation;
            View.OnHitReaction -= StartHitReaction;
        }

        public override void ReturnToPool()
        {
            View.SetDefaultValues();
            Model.SetDefaultValues();
        }
        
        protected virtual void HitBullet() => 
            Deactivation();

        private void HitLaser() => 
            Deactivation();

        private void StartHitReaction(EnemyTypeHit type) =>
            StartCoroutine(HandleHitReaction(type));
        
        private IEnumerator HandleHitReaction(EnemyTypeHit typeHit)
        {
            _mainAudio.PlaySfx(Model.Data.hitClip);
            yield return StartCoroutine(View.PlayHitReaction());
            
            _handlerScore.CountScoreDefeatedEnemy(Model.Data.scoreByHit);

            switch (typeHit)
            {
                case EnemyTypeHit.Bullet:
                    HitBullet();
                    break;
                case EnemyTypeHit.Laser:
                    HitLaser();
                    break;
            }
        }
    }
}
