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

        protected void Deactivation() =>
            OnDeactivation?.Invoke(gameObject);

        public abstract void ReturnToPool();
    }
}
