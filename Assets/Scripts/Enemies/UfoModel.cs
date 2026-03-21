using UnityEngine;

namespace Project.Enemies
{
    public class UfoModel : EnemyModel
    {
        public Vector2 TargetDir { get; private set; }
        
        public void SetTargetDir(Vector2 playerPos,Vector2 ufoPos) =>
            TargetDir = playerPos - ufoPos;

        public override void SetDefaultValues()
        {
            TargetDir = Vector2.zero;
        }
    }
}
