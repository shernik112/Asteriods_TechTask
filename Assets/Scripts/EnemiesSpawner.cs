using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
public class FloatRange
{
        public float Min;
        public float Max;
        public FloatRange(float min, float max)
        {
                Min = min;
                Max = max;
        }
}

public class EnemiesSpawner : ManagedBehaviour
{
        [Inject] private AsteroidPool _asteroidPool;
        [Inject] private UFOPool _ufoPool;
        private FloatRange _rangeTimeAsteroid = new FloatRange(5f,20f);
        private FloatRange _rangeTimeUfo = new FloatRange(10f,25f);

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
                SetPointCreate(_startCountAsteroids);
                _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
                _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
        }

        protected override void ManagedUpdate()
        {
                _lastTimeAsteroid += Time.deltaTime;
                _lastTimeUfo += Time.deltaTime;
                if (_lastTimeAsteroid >= _currentRangeAsteroid)
                {
                        _lastTimeAsteroid = 0f;
                        _currentRangeAsteroid = GetFloatRange(_rangeTimeAsteroid);
                        CreateAsteroid(Random.Range(1,3));
                }

                if (_lastTimeUfo >= _currentRangeUfo)
                {
                        _lastTimeUfo = 0f;
                        _currentRangeUfo = GetFloatRange(_rangeTimeUfo);
                        CreateUfo();
                }
                
        }

        public void CreateAsteroid(int count) => SetPointCreate(count);

        public void CreateUfo()
        {
                var pos = GetRandomPos();
                var obj = _ufoPool.Get();
                obj.transform.position = pos;
        }

        private float GetFloatRange(FloatRange range) => Random.Range(range.Min, range.Max);

        private void SetPointCreate(float count)
        {
                for (int i = 0; i < count; i++)
                {
                        var pos = GetRandomPos();
                        var obj = _asteroidPool.Get();
                        obj.GetComponent<AsteroidBehaviour>().SetDefaultParametrs();
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

        private void RotateAsteroid(GameObject obj)
        {
                var direction = _lookTarget - (Vector2)obj.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(-_rotateOffset,_rotateOffset));
        }
}
