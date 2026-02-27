using System;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause = false;

        private EventBus _eventBus;
            
        [Inject]
        public void Construct(
            EventBus eventBus
        )
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _eventBus.OnRestartGame += TogglePause;
            _eventBus.OnHitPlayer += TogglePause;
        }

        private void OnDestroy()
        {
            _eventBus.OnRestartGame -= TogglePause;
            _eventBus.OnHitPlayer -= TogglePause;
        }

        private void TogglePause()
        {
            IsPause = !IsPause;
        }
    }
}
