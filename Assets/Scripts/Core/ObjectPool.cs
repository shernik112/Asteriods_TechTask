using System.Collections.Generic;
using UnityEngine;

namespace Project.System
{
    public interface IPoolable
    {
        void OnGetFromPool(ObjectPool objectPool);
        void OnReturnToPool();
    }

    public class ObjectPool
    {
        private int StartCount => 5;
        private GameObject _poolPrefab;
        private MainInstaller _installer;
        private Queue<GameObject> _pool = new Queue<GameObject>();

        public ObjectPool(GameObject poolPrefab, MainInstaller installer)
        {
            _poolPrefab = poolPrefab;
            _installer = installer;
            
            MakeInstances();
        }
        
        private void MakeInstances()
        {
            for (var i = 0; i < StartCount; i++)
                CreateObject();
        }

        private void CreateObject()
        {
            var obj = GameObject.Instantiate(_poolPrefab);
            _installer.InjectGo(obj);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        public GameObject Get()
        {
            if(_pool.Count == 0)
                CreateObject();
            var obj = _pool.Dequeue();
            
            if (obj.TryGetComponent<IPoolable>(out var poolable))
                poolable.OnGetFromPool(this);
                
            obj.SetActive(true);
            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            if (obj.TryGetComponent<IPoolable>(out var poolable))
                poolable.OnReturnToPool();
            
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}
