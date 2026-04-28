using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class ObjectPool : IInitializable
    {
        private int StartCount => 5;
        private GameObject _poolPrefab;
        private DiContainer _container;
        private Transform _parentTransform;
        private Queue<GameObject> _pool = new Queue<GameObject>();
        
        public ObjectPool(GameObject poolPrefab, DiContainer container, Transform parentTransform)
        {
            _poolPrefab = poolPrefab;
            _container = container;
            _parentTransform = parentTransform;
        }

        public void Initialize()
        {
            for (var i = 0; i < StartCount; i++)
                CreateObject();
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
