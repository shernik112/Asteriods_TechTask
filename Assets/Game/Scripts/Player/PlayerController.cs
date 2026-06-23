using Project.System;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour, ITeleportedReaction
    {
        private PlayerData _data;
        private RestartButton _restartButton;
        private AudioHandler _audioHandler;

        private PlayerMover _mover;
        private PlayerDeathHandler _deathHandler;

        [Inject]
        public void Construct(
            PlayerData data,
            RestartButton restartButton,
            AudioHandler audioHandler)
        {
            _data = data;
            _restartButton = restartButton;
            _audioHandler = audioHandler;
        }

        private void Awake()
        {
            _mover = GetComponent<PlayerMover>();
            _deathHandler = GetComponent<PlayerDeathHandler>();

            _restartButton.OnRestartGame += Restart;
        }

        private void OnDestroy() =>
            _restartButton.OnRestartGame -= Restart;

        public void TeleportReaction() =>
            _audioHandler.PlaySfx(_data.DashClip);

        private void Restart()
        {
            _deathHandler.ResetState();
            _mover.Stop();
        }
    }
}
