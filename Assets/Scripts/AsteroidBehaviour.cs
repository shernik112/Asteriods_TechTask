using System;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Project.System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Enemies
{
    public class AsteroidBehaviour : BaseEnemy<AsteroidPool>
    {
        private const int COUNT_SHARDS = 2;
    
        [SerializeField] private float multiplierBoost = default;
        [SerializeField] private int countStage = default;
        [SerializeField] private Sprite firstSizeSprite = default;
        [SerializeField] private Sprite secondSizeSpite = default;
        [SerializeField] private Collider2D firstSizeCollider;
        [SerializeField] private Collider2D secondSizeCollider;
        
        private readonly float _createRotate = 50f;
        private readonly float _lowerRotate = 20f;
        private readonly float _defaultSpeed = 1.2f;
        
        private float _currentSpeed;
        private int _sizeLevel = 1;
        private SpriteRenderer _spriteRenderer;
    
        [field: SerializeField] public override int CountScoreByDefeat { get; set; } = default;
    
        private void Awake()
        {
            _currentSpeed = _defaultSpeed;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = firstSizeSprite;
            firstSizeCollider.enabled = true;
            secondSizeCollider.enabled = false;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime, Space.Self);
        }

        private void LateUpdate()
        {
            _spriteRenderer.transform.localRotation = Quaternion.Inverse(transform.rotation);
        }

        private void InitParams(int size)
        {
            _sizeLevel = size;
            if (size == 2)
            {
                _spriteRenderer.sprite = secondSizeSpite;
                firstSizeCollider.enabled = false;
                secondSizeCollider.enabled = true;
            } 
            _currentSpeed += multiplierBoost * size;
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
            
            _spriteRenderer.sprite = firstSizeSprite;
            firstSizeCollider.enabled = true;
            secondSizeCollider.enabled = false;
            
            _currentSpeed = _defaultSpeed;
            transform.rotation = Quaternion.identity;
            _sizeLevel = 1;
        }
    }
}
