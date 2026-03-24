using Project.Enemies;
using Project.System;
using UnityEngine;

namespace Project.Scene
{
    public class TeleportBorder : MonoBehaviour
    {
        private readonly float _teleportOffset = 0.35f;
        private bool _isHorizonWall;

        private void Start()
        {
            _isHorizonWall = transform.position.y != 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IEnemy>(out var enemyObj))
            {
                if (enemyObj.IsFirstEnterToTeleport)
                {
                    enemyObj.IsFirstEnterToTeleport = false;
                    return;
                }
            }

            if (!other.TryGetComponent<ITeleportedReaction>(out var obj))
                return;
            
            obj.TeleportReaction();
            TeleportationObject(other);
        }

        private void TeleportationObject(Collider2D obj)
        {
            Debug.Log($"{typeof(TeleportBorder)} Teleportation Object");
            
            var pos = obj.transform.position;
            
            obj.transform.position = _isHorizonWall
                ? new Vector2(pos.x, GetTargetPos(pos.y))
                : new Vector2(GetTargetPos(pos.x), pos.y);
        }

        private float GetTargetPos(float tpPos)
        {
            return -Mathf.Sign(tpPos) * (Mathf.Abs(tpPos) - _teleportOffset);
        }
    }
}
