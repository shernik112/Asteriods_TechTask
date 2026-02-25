using System;
using Project.System;
using UnityEngine;

namespace Project.Scene
{
    public class TeleportBorder : MonoBehaviour
    {
        private bool _isHorizonWall;
        private readonly float _teleportOffset = 0.35f;

        private void Awake()
        {
            _isHorizonWall = transform.position.y != 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<ITeleported>(out var obj))
                return;
            
            if(obj.TeleportReaction())
                TeleportationObject(other);
        }

        private void TeleportationObject(Collider2D otherObj)
        {
            Debug.Log($"{typeof(TeleportBorder)} Teleportation Object");
            var pos = otherObj.transform.position;
            
            if (_isHorizonWall)
                otherObj.transform.position = new Vector2(pos.x, GetTargetPos(pos.y));
            else
                otherObj.transform.position = new Vector2(GetTargetPos(pos.x), pos.y);
        }

        private float GetTargetPos(float tpPos)
        {
            return -Mathf.Sign(tpPos) * (Mathf.Abs(tpPos) - _teleportOffset);
        }
    }
}
