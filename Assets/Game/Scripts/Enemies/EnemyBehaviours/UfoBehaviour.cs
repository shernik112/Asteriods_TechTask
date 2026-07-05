using System;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.Enemies
{
    public class UfoBehaviour : BaseEnemy
    {
        public event Action OnHitUfo;

        private PlayerMover _playerMover;
        private Vector2 _targetDir;

        [Inject]
        public void ConstructTarget(PlayerMover playerMover)
        {
            _playerMover = playerMover;
        }

        private void Update()
        {
            Vector2 posPlayer = _playerMover.transform.position;
            _targetDir = posPlayer - (Vector2)transform.position;
            _targetDir.Normalize();
        }

        private void FixedUpdate()
        {
            Rb.linearVelocity = _targetDir * enemyData.Speed;
        }

        protected override void HitBullet()
        {
            OnHitUfo?.Invoke();
            Deactivation();
        }
    }
}
