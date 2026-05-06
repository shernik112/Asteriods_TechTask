using Project.Enemies;
using UnityEngine;

namespace Project.System
{
    public class EnemiesSpawner
    {
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
            var angleStep = _data.SpreadRange.angle / _data.CountFragments;
            var halfSpread = _data.SpreadRange.angle / 2f;
            var halfStep = angleStep / 2f;
            var startAngle = -halfSpread + halfStep;

            for (var i = 0; i < _data.CountFragments; i++)
            {
                var baseAngle = startAngle + i * angleStep;
                var offset = Random.Range(-angleStep * _data.FragmentOffset, angleStep * _data.FragmentOffset);
                var finalAngle = baseAngle + offset;

                var spreadRotation = Quaternion.Euler(0f, 0f, finalAngle);
                var fragmentRotation = asteroidTransform.rotation * spreadRotation;

                var fragment = _pools.GetFragmentAsteroid();
                fragment.transform.rotation = fragmentRotation;
                fragment.transform.position = asteroidTransform.position
                                              + fragmentRotation * Vector3.right * _data.SpawnOffset;
            }
        }

        private static void PrepareEnemy(GameObject obj)
        {
            if (obj.TryGetComponent<IEnemy>(out var enemy))
                enemy.IsFirstEnterToTeleport = true;
        }
    }
}