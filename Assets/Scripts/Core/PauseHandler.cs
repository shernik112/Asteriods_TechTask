using System;
using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class PauseHandler : IInitializable, IDisposable
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

        public void Initialize()
        {
            _restartButton.OnRestartGame += TogglePause;
            _playerController.OnHitPlayer += TogglePause;
        }

        public void Dispose()
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
