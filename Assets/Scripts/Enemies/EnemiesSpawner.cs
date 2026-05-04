using Project.Enemies;
using UnityEngine;

namespace Project.System
{
    public class EnemiesSpawner
    {
        private const int CountFragments = 2;

        private readonly EnemiesControllerData _data;
        private readonly EnemyPool _pools;
        
        public EnemiesSpawner(EnemiesControllerData data, EnemyPool pools)
        {
            _data = data;
            _pools = pools;
        }

        public GameObject SpawnAsteroid(Vector2 position, Quaternion rotation)
        {
            var obj = _pools.GetAsteroid();
            PrepareEnemy(obj);

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public GameObject SpawnUfo(Vector2 position, Quaternion rotation)
        {
            var obj = _pools.GetUfo();
            PrepareEnemy(obj);

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public void SpawnFragments(Transform asteroidTransform)
        {
            var sideToggle = false;

            for (var i = 0; i < CountFragments; i++)
            {
                var mag = Random.Range(_data.LowerFragmentRotate, _data.CreateFragmentRotate);
                var fragment = _pools.GetFragmentAsteroid();
                var randomRotate = sideToggle ? mag : -mag;

                PrepareEnemy(fragment);

                fragment.transform.position = asteroidTransform.position;
                fragment.transform.rotation =
                    asteroidTransform.rotation * Quaternion.Euler(0f, 0f, randomRotate);

                sideToggle = !sideToggle;
            }
        }

        private static void PrepareEnemy(GameObject obj)
        {
            if (obj.TryGetComponent<IEnemy>(out var enemy))
                enemy.IsFirstEnterToTeleport = true;
        }
    }
}