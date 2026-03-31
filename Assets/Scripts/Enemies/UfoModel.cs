using UnityEngine;

namespace Project.Enemies
{
    public class UfoModel : EnemyModel
    {
        public Vector2 TargetDir { get; private set; }

        public void SetTargetDir(Vector2 playerPos, Vector2 ufoPos)
        {
            var dir = (playerPos - ufoPos).normalized;
            TargetDir = dir;
        }

        public override void SetDefaultValues()
        {
            TargetDir = Vector2.zero;
        }
    }
}
