using UnityEngine;

namespace Project.Enemies
{
    public class UfoView : EnemyView<UfoModel>
    {
        private void FixedUpdate()
        {
            Rb.linearVelocity = Model.TargetDir * Model.Data.speed;
        }

        public override void SetDefaultValues()
        {
            Rb.linearVelocity = Vector2.zero;
            SpriteRenderer.sprite = Model.Data.sprite;
        }
    }
}
