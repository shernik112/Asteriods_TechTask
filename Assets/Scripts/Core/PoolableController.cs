using System;
using Project.Player;
using Project.System;
using UnityEngine;

namespace Project.Core
{
    public abstract class PoolableController<TModel> : Controller<TModel>, IPoolable
        where TModel : Model
    {
        public event Action<GameObject> OnDeactivation;

        public abstract void ReturnToPool();
        
        protected void Deactivation() =>
            OnDeactivation?.Invoke(gameObject);
    }
}
