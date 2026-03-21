using Project.Core;
using UnityEngine;

namespace Project
{
    public class BulletController : PoolableController<BulletModel>
    {
        [SerializeField] private BulletData data;
        
        public override void ReturnToPool(){}
        
        private BulletView _view;

        protected override void Awake()
        {
            base.Awake();
            _view = GetComponent<BulletView>();
            _view.Init(Model);
        }

        protected override BulletModel CreateModel() =>
            new (data);

        private void OnEnable()
        {
            _view.OnHitReaction += HandleHitReaction;
        }

        private void OnDisable()
        {
            _view.OnHitReaction -= HandleHitReaction;
        }

        private void HandleHitReaction() =>
            Deactivation();
    }
}
