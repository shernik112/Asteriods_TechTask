using System;
using UnityEngine;

namespace Project.System
{
    public class EventBus : MonoBehaviour
    {
        public Action OnRestartGame;
        public Action OnHitPlayer;
        public Action<int> IsFinalScore;
        public Action<int> NewTargetScore;
        public Action<int> NewCountShotLaser;
    }
}
