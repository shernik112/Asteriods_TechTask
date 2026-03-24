using System.Collections.Generic;
using UnityEngine;

namespace Project.System
{
    public class ObjectPool
    {
        private int StartCount => 5;
        private readonly GameObject _poolPrefab;
        private readonly PlaySceneInstaller _installer;
        private readonly Transform _parentTransform;
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        
        public ObjectPool(GameObject poolPrefab, PlaySceneInstaller installer, Transform parentTransform)
        {
            _poolPrefab = poolPrefab;
            _installer = installer;
            _parentTransform = parentTransform;
            
            MakeInstances();
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
        
        private void MakeInstances()
        {
            for (var i = 0; i < StartCount; i++)
                CreateObject();
        }

        private void CreateObject()
        {
            var obj = _installer.Instantiate(_poolPrefab, _parentTransform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        private void ReturnObjectToPool(GameObject obj)
        {
            if (obj.TryGetComponent<IPoolable>(out var poolable))
            {
                poolable.ReturnToPool();
                poolable.OnDeactivation -= ReturnObjectToPool;
            }
            
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}
