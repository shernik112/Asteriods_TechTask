using Project.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.System
{
    public class EnemiesSpawner : MonoBehaviour
    {
        public  ObjectPool AsteroidPool;
        public ObjectPool UfoPool;
        
        private GameObject _asteroidPoolPrefab;
        private GameObject _ufoPoolPrefab;
        private MainInstaller _mainInstaller;
        private RestartButton _restartButton;
        private PauseHandler _pauseHandler;
        private FloatRange _rangeTimeAsteroid = new FloatRange(5f, 10f);
        private FloatRange _rangeTimeUfo = new FloatRange(5f, 15f);

        private readonly float _rotateOffset = 30f;
        private readonly float _posOffset = 0.5f;
        private readonly Vector2 _lookTarget = new Vector2(0, 0);
        private readonly int _startCountAsteroids = 2;

        private float _halfHeight;
        private float _halfWidth;
        private float _lastTimeAsteroid;
        private float _lastTimeUfo;
        private float _currentRangeAsteroid;
        private float _currentRangeUfo;

        [Inject]
        public void Construct(
            [Inject(Id = "Asteroid")] GameObject asteroidPrefab,
            [Inject(Id = "Ufo")] GameObject ufoPrefab,
            RestartButton restartButton,
            PauseHandler pauseHandler,
            MainInstaller mainInstaller)
        {
            _asteroidPoolPrefab = asteroidPrefab;
            _ufoPoolPrefab = ufoPrefab;
            _restartButton = restartButton;
            _pauseHandler = pauseHandler;
            _mainInstaller = mainInstaller;
        }
        
        private void Awake()
        {
            _restartButton.OnRestartGame += StartCreate;   
            AsteroidPool = new ObjectPool(_asteroidPoolPrefab, _mainInstaller);
            UfoPool = new ObjectPool(_ufoPoolPrefab, _mainInstaller);
        }
        
        private void OnDestroy()
        {
            _restartButton.OnRestartGame -= StartCreate;
        }

        private void Start()
        {
            var cam = Camera.main;
            if (cam != null)
            {
                _halfHeight = cam.orthographicSize;
                _halfWidth = _halfHeight * cam.aspect;
                _halfHeight += _posOffset;
                _halfWidth += _posOffset;
            }
            
            StartCreate();
        }

        private void Update()
        {
            if (_pauseHandler.IsPause)
                return;

            _lastTimeAsteroid += Time.deltaTime;
            _lastTimeUfo += Time.deltaTime;

            if (_lastTimeAsteroid >= _currentRangeAsteroid)
            {
                _lastTimeAsteroid = 0f;
                _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
                CreateAsteroid(Random.Range(2, 3));
            }

            if (_lastTimeUfo >= _currentRangeUfo)
            {
                _lastTimeUfo = 0f;
                _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
                CreateUfo();
            }
        }
        
        private void StartCreate()
        {
            SetPointCreate(_startCountAsteroids);
            _lastTimeAsteroid = default;
            _lastTimeUfo = default;
            _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
            _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
        }

        private void CreateAsteroid(int count) =>
            SetPointCreate(count);

        private void CreateUfo()
        {
            var pos = GetRandomPos();
            var obj = UfoPool.Get();
            ActiveBoolFieldForTeleportation(obj);
            obj.transform.position = pos;
        }

        private float GetFloatRange(FloatRange range) =>
            Random.Range(range.Min, range.Max);

        private void SetPointCreate(float count)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = GetRandomPos();
                var obj = AsteroidPool.Get();
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
    }
}
