using UnityEngine;
using Zenject;

namespace Project.System
{
    public class EnemyPool
    {
        private readonly EnemiesControllerData _data;
        private readonly DiContainer _container;
        private readonly Transform _poolsRoot;

        private readonly ObjectPool _asteroidPool;
        private readonly ObjectPool _fragmentAsteroidPool;
        private readonly ObjectPool _ufoPool;

        public EnemyPool(
            EnemiesControllerData data,
            DiContainer container,
            Transform poolsRoot)
        {
            _data = data;
            _container = container;
            _poolsRoot = poolsRoot;
            
            _asteroidPool = new ObjectPool(_data.AsteroidPrefab, _container, _poolsRoot);
            _fragmentAsteroidPool = new ObjectPool(_data.FragmentAsteroidPrefab, _container, _poolsRoot);
            _ufoPool = new ObjectPool(_data.UfoPrefab, _container, _poolsRoot);
        }
        
        public GameObject GetAsteroid() => 
            _asteroidPool.Get();
        
        public GameObject GetFragmentAsteroid() => 
            _fragmentAsteroidPool.Get();
        
        public GameObject GetUfo() => 
            _ufoPool.Get();
    }
}