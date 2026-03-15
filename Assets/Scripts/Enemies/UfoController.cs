using UnityEngine;

namespace Project.Enemies
{
    public class UfoController : EnemyController<UfoModel>
    {
        private void Update()
        {
            Vector2 posPlayer = PlayerController.View.gameObject.transform.position;
            Model.SetTargetDir(posPlayer,View.transform.position);
        }

        protected override UfoModel CreateModel() =>
            new (enemyData);
    }
}
