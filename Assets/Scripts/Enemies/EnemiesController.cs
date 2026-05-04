using System;
using Project.Enemies;
using Project.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.System
{
    public class EnemiesController : IInitializable, ITickable, IDisposable
    {
        private readonly FloatRange _rangeTimeAsteroid = new FloatRange(5f, 10f);
        private readonly FloatRange _rangeTimeUfo = new FloatRange(5f, 15f);

        private readonly int _startCountAsteroids = 2;
        private readonly float _posOffset = 0.5f;

        private readonly EnemiesControllerData _data;
        private readonly Camera _mainCamera;
        private readonly RestartButton _restartButton;
        private readonly PauseHandler _pauseHandler;
        private readonly EnemiesSpawnArea _enemiesSpawnArea;
        private readonly EnemyPool _pools;
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
            DiContainer container,
            Transform poolsRoot)
        {
            _data = data;
            _mainCamera = mainCamera;
            _restartButton = restartButton;
            _pauseHandler = pauseHandler;

            _enemiesSpawnArea = new EnemiesSpawnArea(data, mainCamera);
            _pools = new EnemyPool(data, container, poolsRoot);
            _enemiesSpawner = new EnemiesSpawner(_data, _pools);
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
                _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
                SpawnAsteroids(Random.Range(2, 4));
            }

            if (_lastTimeUfo >= _currentRangeUfo)
            {
                _lastTimeUfo = 0f;
                _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
                SpawnUfo();
            }
        }

        public void Dispose()
        {
            _restartButton.OnRestartGame -= StartCreate;
        }

        private void StartCreate()
        {
            SpawnAsteroids(_startCountAsteroids);

            _lastTimeAsteroid = 0f;
            _lastTimeUfo = 0f;
            _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
            _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
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

            if (obj.TryGetComponent<AsteroidBehaviour>(out var asteroid))
            {
                asteroid.OnHitAsteroid -= _enemiesSpawner.SpawnFragments;
                asteroid.OnHitAsteroid += _enemiesSpawner.SpawnFragments;
            }
        }

        private void SpawnUfo()
        {
            var pos = _enemiesSpawnArea.GetRandomEdgePosition();
            _enemiesSpawner.SpawnUfo(pos, Quaternion.identity);
        }

        private float GetFloatRange(FloatRange range) =>
            Random.Range(range.Min, range.Max);
    }
}