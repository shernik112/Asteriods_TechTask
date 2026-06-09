using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class ObjectPool
    { 
        private readonly GameObject _poolPrefab;
        private readonly IInstantiator _container;
        private readonly Transform _parentTransform;
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        
        public ObjectPool(
            GameObject poolPrefab, 
            IInstantiator container, 
            Transform parentTransform)
        {
            _poolPrefab = poolPrefab;
            _container = container;
            _parentTransform = parentTransform;
        }

        private void CreateObject()
        {
            var obj = _container.InstantiatePrefab(_poolPrefab, _parentTransform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        public GameObject Get()
        {
            if(_pool.Count == 0)
                CreateObject();
            var obj = _pool.Dequeue();

            if (obj.TryGetComponent<IPoolable>(out var poolable))
                poolable.OnDeactivation += ReturnObjectToPool;
            
            obj.SetActive(true);
            return obj;
        }

        private void ReturnObjectToPool(GameObject obj)
        {
            if (obj.TryGetComponent<IPoolable>(out var poolable))
            {
                poolable.OnReturnToPool();
                poolable.OnDeactivation -= ReturnObjectToPool;
            }
            
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}
