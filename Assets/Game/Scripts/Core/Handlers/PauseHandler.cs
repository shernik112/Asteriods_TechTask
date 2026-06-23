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

        private PlayerDeathHandler _deathHandler;
        private RestartButton _restartButton;

        [Inject]
        public void Construct(
            PlayerDeathHandler deathHandler,
            RestartButton restartButton
        )
        {
            _deathHandler = deathHandler;
            _restartButton = restartButton;
        }

        public void Initialize()
        {
            _restartButton.OnRestartGame += TogglePause;
            _deathHandler.OnHitPlayer += TogglePause;
        }

        public void Dispose()
        {
            _restartButton.OnRestartGame -= TogglePause;
            _deathHandler.OnHitPlayer -= TogglePause;
        }

        private void TogglePause()
        {
                IsPause = !IsPause;
            Debug.Log($"{typeof(PauseHandler)} {IsPause}");
        }
    }
}
