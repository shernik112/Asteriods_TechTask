using System.Collections;
using Project.Enemies;
using Project.Player;
using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Scene
{
    public class TeleportBorder : MonoBehaviour
    {
        [SerializeField] private bool isHorizonWall = default;
        
        private readonly float _teleportOffset = 0.35f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<ITeleported>(out var obg))
                return;
            
            if(obg.TeleportReaction())
                TeleportationObject(other);
        }

        private void TeleportationObject(Collider2D otherObj)
        {
            var pos = otherObj.transform.position;
            
            if (isHorizonWall)
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
