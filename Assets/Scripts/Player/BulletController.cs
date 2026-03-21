using Project.Core;
using UnityEngine;

namespace Project
{
    public class BulletController : PoolableController<BulletModel>
    {
        public override void ReturnToPool(){}
        
        [SerializeField] private BulletData data;
        
        private BulletView _view;

        protected override void Awake()
        {
            base.Awake();
            _view = GetComponent<BulletView>();
            _view.Init(Model);
        }
        
        private void OnEnable()
        {
            _view.OnHitReaction += HandleHitReaction;
        }

        private void OnDisable()
        {
            _view.OnHitReaction -= HandleHitReaction;
        }

        protected override BulletModel CreateModel()
        {
            var model = ScriptableObject.CreateInstance<BulletModel>();
            model.Init(data);
            return model;
        }

        private void HandleHitReaction() =>
            Deactivation();
    }
}
