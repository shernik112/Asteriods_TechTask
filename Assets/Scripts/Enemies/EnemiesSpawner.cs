using System;
using Random = UnityEngine.Random;
using Project.Enemies;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class EnemiesSpawner : IInitializable, ITickable, IDisposable
    {
        private const int COUNT_FRAGMENTS = 2;

        private readonly float _rotateOffset = 30f;
        private readonly float _posOffset = 0.5f;
        private readonly Vector2 _lookTarget = Vector2.zero;
        private readonly int _startCountAsteroids = 2;
        private readonly float _createFragmentRotate = 50f;
        private readonly float _lowerFragmentRotate = 20f;

        private readonly EnemiesSpawnerData _data;
        private readonly Camera _mainCamera;
        private readonly RestartButton _restartButton;
        private readonly PauseHandler _pauseHandler;
        private readonly DiContainer _container;
        private readonly Transform _poolsRoot;

        private readonly FloatRange _rangeTimeAsteroid = new FloatRange(5f, 10f);
        private readonly FloatRange _rangeTimeUfo = new FloatRange(5f, 15f);

        private ObjectPool _asteroidPool;
        private ObjectPool _fragmentAsteroidPool;
        private ObjectPool _ufoPool;

        private float _halfHeight;
        private float _halfWidth;
        private float _lastTimeAsteroid;
        private float _lastTimeUfo;
        private float _currentRangeAsteroid;
        private float _currentRangeUfo;

        public EnemiesSpawner(
            EnemiesSpawnerData data,
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
            _container = container;
            _poolsRoot = poolsRoot;
        }

        public void Initialize()
        {
            _halfHeight = _mainCamera.orthographicSize + _posOffset;
            _halfWidth = _halfHeight * _mainCamera.aspect + _posOffset;

            _asteroidPool = new ObjectPool(_data.asteroidPrefab, _container, _poolsRoot);
            _fragmentAsteroidPool = new ObjectPool(_data.fragmentAsteroidPrefab, _container, _poolsRoot);
            _ufoPool = new ObjectPool(_data.ufoPrefab, _container, _poolsRoot);

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
                CreateAsteroid(Random.Range(2, 4));
            }

            if (_lastTimeUfo >= _currentRangeUfo)
            {
                _lastTimeUfo = 0f;
                _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
                CreateUfo();
            }
        }

        public void Dispose()
        {
            _restartButton.OnRestartGame -= StartCreate;
        }

        private void StartCreate()
        {
            SetPointCreate(_startCountAsteroids);
            _lastTimeAsteroid = 0f;
            _lastTimeUfo = 0f;
            _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
            _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
        }

        private void CreateAsteroid(int count) =>
            SetPointCreate(count);

        private void CreateUfo()
        {
            var pos = GetRandomPos();
            var obj = _ufoPool.Get();
            ActiveBoolFieldForTeleportation(obj);
            obj.transform.position = pos;
        }

        private float GetFloatRange(FloatRange range) =>
            Random.Range(range.Min, range.Max);

        private void SetPointCreate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = GetRandomPos();
                var obj = _asteroidPool.Get();

                if (obj.TryGetComponent<AsteroidBehaviour>(out var asteroid))
                {
                    asteroid.OnHitAsteroid -= InitFragmentsAsteroid;
                    asteroid.OnHitAsteroid += InitFragmentsAsteroid;
                }

                ActiveBoolFieldForTeleportation(obj);
                obj.transform.position = pos;
                RotateAsteroid(obj);
            }
        }

        private Vector2 GetRandomPos()
        {
            var side = Random.Range(0, 4);
            return side switch
            {
                0 => new Vector2(_halfWidth, Random.Range(-_halfHeight, _halfHeight)),
                1 => new Vector2(-_halfWidth, Random.Range(-_halfHeight, _halfHeight)),
                2 => new Vector2(Random.Range(-_halfWidth, _halfWidth), _halfHeight),
                3 => new Vector2(Random.Range(-_halfWidth, _halfWidth), -_halfHeight),
                _ => Vector2.zero
            };
        }

        private void ActiveBoolFieldForTeleportation(GameObject obj) =>
            obj.GetComponent<IEnemy>().IsFirstEnterToTeleport = true;

        private void RotateAsteroid(GameObject obj)
        {
            var direction = _lookTarget - (Vector2)obj.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            obj.transform.rotation = Quaternion.Euler(
                0f,
                0f,
                angle + Random.Range(-_rotateOffset, _rotateOffset));
        }

        private void InitFragmentsAsteroid(Transform asteroidTransform)
        {
            var sideToggle = false;

            for (var i = 0; i < COUNT_FRAGMENTS; i++)
            {
                var mag = Random.Range(_lowerFragmentRotate, _createFragmentRotate);
                var obj = _fragmentAsteroidPool.Get();
                var randomRotate = sideToggle ? mag : -mag;

                obj.transform.position = asteroidTransform.position;
                obj.transform.rotation = asteroidTransform.rotation * Quaternion.Euler(0f, 0f, randomRotate);

                sideToggle = !sideToggle;
            }
        }
    }
}