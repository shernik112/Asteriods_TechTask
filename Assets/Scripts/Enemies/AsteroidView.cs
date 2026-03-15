using System;
using UnityEngine;

namespace Project.Enemies
{
    public class AsteroidView : EnemyView<AsteroidModel>
    {
        private void FixedUpdate()
        {
            Rb.linearVelocity = transform.right * Model.Data.speed;
        }

        private void LateUpdate()
        {
            SpriteRenderer.transform.rotation = Quaternion.identity;
        }

        public override void SetDefaultValues()
        {
            Rb.linearVelocity = Vector2.zero;
            Rb.angularVelocity = 0f;
            transform.localRotation = Quaternion.identity;
            SpriteRenderer.sprite = Model.Data.sprite;
        }
    }
}
