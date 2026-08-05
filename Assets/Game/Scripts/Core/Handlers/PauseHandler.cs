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

        private readonly PlayerDeathHandler _deathHandler;
        private readonly RestartButton _restartButton;
        private readonly BlockCursor _blockCursor;
        
        public PauseHandler(
            PlayerDeathHandler deathHandler,
            RestartButton restartButton,
            BlockCursor blockCursor
        )
        {
            _deathHandler = deathHandler;
            _restartButton = restartButton;
            _blockCursor = blockCursor;
        }

        public void Initialize()
        {
            _restartButton.OnRestartGame += TogglePause;
            _deathHandler.OnHitPlayer += TogglePause;
            
            _blockCursor.SetLocked(!IsPause);
        }

        public void Dispose()
        {
            _restartButton.OnRestartGame -= TogglePause;
            _deathHandler.OnHitPlayer -= TogglePause;
        }

        private void TogglePause()
        {
            IsPause = !IsPause;
            
            _blockCursor.SetLocked(!IsPause);
            Debug.Log($"{typeof(PauseHandler)} {IsPause}");
        }
        
    }
}
