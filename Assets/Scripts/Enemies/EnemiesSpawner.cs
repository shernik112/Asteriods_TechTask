using Random = UnityEngine.Random;
using Project.Enemies;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class EnemiesSpawner : MonoBehaviour
    {
        private const int COUNT_FRAGMENTS = 2;
        
        private readonly float _rotateOffset = 30f;
        private readonly float _posOffset = 0.5f;
        private readonly Vector2 _lookTarget = new Vector2(0, 0);
        private readonly int _startCountAsteroids = 2;
        private readonly float _createFragmentRotate = 50f;
        private readonly float _lowerFragmentRotate = 20f;
        
        private ObjectPool _asteroidPool;
        private ObjectPool _fragmentAsteroidPool;
        private ObjectPool _ufoPool;
        private Camera _mainCamera;
        private GameObject _asteroidPrefab;
        private GameObject _fragmentAsteroidPrefab;
        private GameObject _ufoPrefab;
        private MainInstaller _mainInstaller;
        private EventBus _eventBus;
        private PauseHandler _pauseHandler;
        private FloatRange _rangeTimeAsteroid = new FloatRange(5f, 10f);
        private FloatRange _rangeTimeUfo = new FloatRange(5f, 15f);
        
        private float _halfHeight;
        private float _halfWidth;
        private float _lastTimeAsteroid;
        private float _lastTimeUfo;
        private float _currentRangeAsteroid;
        private float _currentRangeUfo;

        [Inject]
        public void Construct(
            [Inject(Id = "Asteroid")] GameObject asteroidPrefab,
            [Inject(Id = "FragmentAsteroid")] GameObject fragmentAsteroidPrefab,
            [Inject(Id = "Ufo")] GameObject ufoPrefab,
            Camera mainCamera,
            EventBus eventBus,
            PauseHandler pauseHandler,
            MainInstaller mainInstaller)
        {
            _asteroidPrefab = asteroidPrefab;
            _fragmentAsteroidPrefab = fragmentAsteroidPrefab;
            _ufoPrefab = ufoPrefab;
            _mainCamera = mainCamera;
            _eventBus = eventBus;
            _pauseHandler = pauseHandler;
            _mainInstaller = mainInstaller;
        }
        
        private void Awake()
        {
            _eventBus.OnRestartGame += StartCreate;   
            _asteroidPool = new ObjectPool(_asteroidPrefab, _mainInstaller,transform);
            _fragmentAsteroidPool = new ObjectPool(_fragmentAsteroidPrefab, _mainInstaller, transform);
            _ufoPool = new ObjectPool(_ufoPrefab, _mainInstaller,transform);
        }
        
        private void OnDestroy()
        {
            _eventBus.OnRestartGame -= StartCreate;
        }

        private void Start()
        {
            _halfHeight = _mainCamera.orthographicSize;
            _halfWidth = _halfHeight * _mainCamera.aspect;
            _halfHeight += _posOffset;
            _halfWidth += _posOffset;
            
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
            var obj = _ufoPool.Get();
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
                var obj = _asteroidPool.Get();
                if (obj.TryGetComponent<AsteroidBehaviour>(out var asteroid))
                    asteroid.OnHitAsteroid += InitFragmentsAsteroid;
                
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
            if (!obj.TryGetComponent<Rigidbody2D>(out var rb))
               return;
            
            var direction = _lookTarget - (Vector2)obj.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var offset = Random.Range(-_rotateOffset, _rotateOffset);
            
            rb.rotation = angle + offset;
        }
        
        private void InitFragmentsAsteroid(Transform asteroidTransform)
        {
            var sideToggle = false;
            for (var i = 0; i < COUNT_FRAGMENTS; i++)
            { 
                var mag = Random.Range(_lowerFragmentRotate, _createFragmentRotate);
                var obj = _fragmentAsteroidPool.Get();
                
                if (!obj.TryGetComponent<Rigidbody2D>(out var rb))
                    return;
                
                rb.position = asteroidTransform.position;
                var baseAngle = transform.eulerAngles.z;
                var randomRotate = sideToggle ? mag : -mag;
                rb.rotation = baseAngle + randomRotate;
                
                sideToggle = !sideToggle;
            }
        }
    }
}
