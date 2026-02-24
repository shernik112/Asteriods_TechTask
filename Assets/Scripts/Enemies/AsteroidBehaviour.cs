using Project.System;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using UnityEngine;

namespace Project.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidBehaviour : BaseEnemy
    {
        private const int COUNT_SHARDS = 2;
    
        [SerializeField] private float multiplierBoost = default;
        [SerializeField] private int countStage = default;
        [SerializeField] private Sprite firstSizeSprite = default;
        [SerializeField] private Sprite secondSizeSpite = default;
        [SerializeField] private Sprite firstHitSprite = default;
        [SerializeField] private Sprite secondHitSprite = default;
        [SerializeField] private Collider2D firstSizeCollider;
        [SerializeField] private Collider2D secondSizeCollider;
        
        private readonly float _createRotate = 50f;
        private readonly float _lowerRotate = 20f;
        private readonly float _defaultSpeed = 1f;
        
        private float _currentSpeed;
        private int _sizeLevel = 1;
    
        [field: SerializeField] public override int CountScoreByDefeat { get; set; } = default;

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
            _currentSpeed = _defaultSpeed;
            SpriteRenderer.sprite = firstSizeSprite;
            HitSprite = firstHitSprite;
            firstSizeCollider.enabled = true;
            secondSizeCollider.enabled = false;
        }

        private void FixedUpdate()
        {
            Rb.linearVelocity = transform.right * _currentSpeed;
        }

        private void LateUpdate()
        {
            SpriteRenderer.transform.localRotation = Quaternion.Inverse(transform.rotation);
        }

        public override void OnReturnToPool()
        {
            SetDefaultParameters();
        }

        private void InitParams(int size)
        {
            _sizeLevel = size;
            if (size == 2)
            {
                SpriteRenderer.sprite = secondSizeSpite;
                HitSprite = secondHitSprite;
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

        private void SetDefaultParameters()
        {
            if (TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            
            SpriteRenderer.sprite = firstSizeSprite;
            HitSprite = firstHitSprite;
            firstSizeCollider.enabled = true;
            secondSizeCollider.enabled = false;
            
            _currentSpeed = _defaultSpeed;
            transform.localRotation = Quaternion.identity;
            _sizeLevel = 1;
        }
    }
}
