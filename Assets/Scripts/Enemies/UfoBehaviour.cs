using System;
using UnityEngine;

namespace Project.Enemies
{
    public class UfoBehaviour : BaseEnemy
    {
        [SerializeField] private float speed = default;
        [SerializeField] private Sprite hitSprite = default;

        private Vector2 _targetDir;
        
        [field:SerializeField] public override int CountScoreByDefeat { get; set; } = default;

        protected override void Awake()
        {
            base.Awake();
            HitSprite = hitSprite;
        }

        private void Update()
        {
            Vector2 posPlayer = PlayerController.gameObject.transform.position;
            _targetDir = posPlayer - (Vector2)transform.position;
            _targetDir.Normalize();
        }

        private void FixedUpdate()
        {
            Rb.linearVelocity = _targetDir * speed;
        }
    }
}
