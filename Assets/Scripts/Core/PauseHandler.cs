using System;
using NUnit.Framework;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause { get; private set; }

        private PlayerController _playerController;
        private RestartButton _restartButton;
            
        [Inject]
        public void Construct(
            PlayerController playerController,
            RestartButton restartButton
        )
        {
            _playerController = playerController;
            _restartButton = restartButton;
        }

        private void Awake()
        {
            _restartButton.OnRestartGame += TogglePause;
            _playerController.OnHitPlayer += TogglePause;
        }

        private void OnDestroy()
        {
            _restartButton.OnRestartGame -= TogglePause;
            _playerController.OnHitPlayer -= TogglePause;
        }

        private void TogglePause()
        {
                IsPause = !IsPause;
            Debug.Log($"{typeof(PauseHandler)} {IsPause}");
        }
    }
}
