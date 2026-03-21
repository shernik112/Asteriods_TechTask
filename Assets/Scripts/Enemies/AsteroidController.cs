using System;
using UnityEngine;

namespace Project.Enemies
{
    public class AsteroidController : EnemyController<AsteroidModel>
    {
        public event Action<Transform> OnHitAsteroid;
        
        public override void ReturnToPool()
        {
            base.ReturnToPool();
            OnHitAsteroid = null;
        }
        
        protected override void HitBullet()
        {
            OnHitAsteroid?.Invoke(transform);
            Deactivation();
        }

        protected override AsteroidModel CreateModel()
        {
            var m = ScriptableObject.CreateInstance<AsteroidModel>();
            m.Init(enemyData);
            return m;
        }
    }
}
