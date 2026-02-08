using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Project.System;
using UnityEngine;

namespace Project.Enemies
{
    public class AsteroidBehaviour : BaseEnemy<AsteroidPool>
    {
        private const int COUNT_SHARDS = 2;
    
        [SerializeField] private float multiplierBoost = default;
        [SerializeField] private int countStage = default;

        private readonly float _createRotate = 50f;
        private readonly float _lowerRotate = 20f;
        private readonly float _defaultSpeed = 1.2f;
    
        private Vector3 _initialScale;
        private float _currentSpeed;
        private int _sizeLevel = 1;
    
        [field: SerializeField] public override int CountScoreByDefeat { get; set; } = default;
    
        private void Awake()
        {
            _initialScale = transform.localScale;
            _currentSpeed = _defaultSpeed;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime, Space.Self);
        }

        private void InitParams(int size)
        {
            _sizeLevel = size;
            _currentSpeed += multiplierBoost * size;
            transform.localScale =  _initialScale / size;
        }

        protected override void HitBullet()
        {
            if (_sizeLevel >= countStage)
            {
                Pool.ReturnToPool(gameObject); 
                return;
            }
        
            InitAsteroid();
            Pool.ReturnToPool(gameObject);
        }

        private void InitAsteroid()
        {
            var sideToggle = false;
            for (var i = 0; i < COUNT_SHARDS; i++)
            { 
                var mag = Random.Range(_lowerRotate, _createRotate);
                var obj = Pool.Get();
                var randomRotate = sideToggle ? mag : -mag;
                obj.GetComponent<AsteroidBehaviour>().InitParams(_sizeLevel + 1);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, randomRotate);
                sideToggle = !sideToggle;
            }
        }

        public void SetDefaultParameters()
        {
            if (TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        
            _currentSpeed = _defaultSpeed;
            transform.localScale = _initialScale;
            transform.rotation = Quaternion.identity;
            _sizeLevel = 1;
        }
    }
}
