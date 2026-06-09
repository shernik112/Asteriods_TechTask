using UnityEngine;
using Zenject;

namespace Project.System
{
    public class EnemyPool
    {
        private readonly ObjectPool _asteroidPool;
        private readonly ObjectPool _fragmentAsteroidPool;
        private readonly ObjectPool _ufoPool;

        public EnemyPool(
            EnemiesControllerData data,
            IInstantiator container,
            Transform poolsRoot)
        {
            _asteroidPool = new ObjectPool(data.AsteroidPrefab, container, poolsRoot);
            _fragmentAsteroidPool = new ObjectPool(data.FragmentAsteroidPrefab, container, poolsRoot);
            _ufoPool = new ObjectPool(data.UfoPrefab, container, poolsRoot);
        }
        
        public GameObject GetAsteroid() => 
            _asteroidPool.Get();
        
        public GameObject GetFragmentAsteroid() => 
            _fragmentAsteroidPool.Get();
        
        public GameObject GetUfo() => 
            _ufoPool.Get();
    }
}