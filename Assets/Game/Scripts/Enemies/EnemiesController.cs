using Random = UnityEngine.Random;
using System;
using Project.Enemies;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class EnemiesController : IInitializable, ITickable, IDisposable
    {
        private readonly EnemiesControllerData _data;
        private readonly RestartButton _restartButton;
        private readonly PauseHandler _pauseHandler;
        private readonly EnemiesSpawnArea _enemiesSpawnArea;
        private readonly EnemiesSpawner _enemiesSpawner;

        private float _lastTimeAsteroid;
        private float _lastTimeUfo;
        private float _currentRangeAsteroid;
        private float _currentRangeUfo;

        public EnemiesController(
            EnemiesControllerData data,
            Camera mainCamera,
            RestartButton restartButton,
            PauseHandler pauseHandler,
            IInstantiator  container,
            Transform poolsRoot)
        {
            _data = data;
            _restartButton = restartButton;
            _pauseHandler = pauseHandler;

            _enemiesSpawnArea = new EnemiesSpawnArea(data, mainCamera);

            var pools = new EnemyPool(data, container, poolsRoot);
            _enemiesSpawner = new EnemiesSpawner(data, pools);
        }

        public void Initialize()
        {
            _restartButton.OnRestartGame += StartCreate;

            StartCreate();
        }

        public void Tick()
        {
            if (_pauseHandler.IsPause)
                return;

            _lastTimeAsteroid += Time.deltaTime;
            _lastTimeUfo += Time.deltaTime;

            if (_lastTimeAsteroid >= _currentRangeAsteroid)
            {
                _lastTimeAsteroid = 0f;
                _currentRangeAsteroid = GetFloatRange(_data.RangeTimeAsteroid);

                SpawnAsteroids(Random.Range(2, 4));
            }

            if (!(_lastTimeUfo >= _currentRangeUfo))
                return;

            _lastTimeUfo = 0f;
            _currentRangeUfo = GetFloatRange(_data.RangeTimeUfo);
            SpawnUfo();
        }

        public void Dispose()
        {
            _restartButton.OnRestartGame -= StartCreate;
        }

        private void StartCreate()
        {
            SpawnAsteroids(_data.StartCountAsteroids);

            _lastTimeAsteroid = 0f;
            _lastTimeUfo = 0f;
            _currentRangeAsteroid = GetFloatRange(_data.RangeTimeAsteroid);
            _currentRangeUfo = GetFloatRange(_data.RangeTimeUfo);
        }

        private void SpawnAsteroids(int count)
        {
            for (var i = 0; i < count; i++)
                SpawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            var pos = _enemiesSpawnArea.GetRandomEdgePosition();
            var rot = _enemiesSpawnArea.GetAsteroidRotation(pos);

            var obj = _enemiesSpawner.SpawnAsteroid(pos, rot);

            if (!obj.TryGetComponent<AsteroidBehaviour>(out var asteroid))
                return;

            asteroid.OnHitAsteroid -= HandleHitAsteroid;
            asteroid.OnHitAsteroid += HandleHitAsteroid;
        }
        
        private void HandleHitAsteroid(Transform transform) =>
            _enemiesSpawner.SpawnFragments(transform);
            
        private void SpawnUfo()
        {
            var pos = _enemiesSpawnArea.GetRandomEdgePosition();
            _enemiesSpawner.SpawnUfo(pos, Quaternion.identity);
        }

        private float GetFloatRange(FloatRange range) =>
            Random.Range(range.min, range.max);
    }
}