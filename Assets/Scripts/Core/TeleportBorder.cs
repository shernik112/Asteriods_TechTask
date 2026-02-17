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
        [SerializeField] private AudioClip playerDashClip = default;


        private readonly float _teleportOffset = 0.35f;

        private MainAudio _mainAudio;

        [Inject]
        public void Construct(MainAudio mainAudio)
        {
            _mainAudio = mainAudio;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IEnemy>(out var enemy))
            {
                if (enemy.IsFirstEnterToTeleport)
                {
                    enemy.IsFirstEnterToTeleport = false;
                    return;
                }
            
                TeleportationObject(other);
            }

            else if (other.TryGetComponent<PlayerController>(out var player))
            {
                TeleportationObject(other);
                _mainAudio.PlaySfx(playerDashClip);
            }
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
