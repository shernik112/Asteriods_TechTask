using System;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause { get; private set; }

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
            Debug.Log($"{typeof(PauseHandler)} {IsPause}");
        }
    }
}
