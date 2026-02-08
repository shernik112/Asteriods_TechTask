using Project.System;
using UnityEngine;

namespace Project.Enemies
{
    public class UfoBehaviour : BaseEnemy<UfoPool>
    {
        [SerializeField] private float speed = default;
        [field:SerializeField] public override int CountScoreByDefeat { get; set; } = default;

        private void Update()
        {
            Vector2 posPlayer = PlayerController.gameObject.transform.position;
            var targetDir = posPlayer - (Vector2)transform.position;
            targetDir.Normalize();
            transform.Translate(targetDir * speed * Time.deltaTime, Space.World);
        }
    }
}
