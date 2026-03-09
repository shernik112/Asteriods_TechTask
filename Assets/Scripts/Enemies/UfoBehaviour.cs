using UnityEngine;

namespace Project.Enemies
{
    public class UfoBehaviour : BaseEnemy
    {
        private Vector2 _targetDir;
        
        private void Update()
        {
            Vector2 posPlayer = PlayerController.View.gameObject.transform.position;
            _targetDir = posPlayer - (Vector2)transform.position;
            _targetDir.Normalize();
        }

        private void FixedUpdate()
        {
            Rb.linearVelocity = _targetDir * enemyData.speed;
        }
    }
}
