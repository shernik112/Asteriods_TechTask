using System;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause = false;

        private RestartButton _restartButton;
        private PlayerController _characterController;
            
        [Inject]
        public void Construct(
            RestartButton restartButton,
            PlayerController characterController
        )
        {
            _restartButton = restartButton;
            _characterController = characterController;
        }

        private void Awake()
        {
            _restartButton.OnRestartGame += TogglePause;
            _characterController.OnHitPlayer += TogglePause;
        }

        private void OnDestroy()
        {
            _restartButton.OnRestartGame -= TogglePause;
            _characterController.OnHitPlayer -= TogglePause;
        }

        private void TogglePause()
        {
            IsPause = !IsPause;
        }
    }
}
