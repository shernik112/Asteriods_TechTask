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
        private MainAudio _mainAudio;

        public PlayerMover Mover { get; private set; }
        public PlayerDeathHandler DeathHandler { get; private set; }
        public ShootLaser Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }

        [Inject]
        public void Construct(
            PlayerData data,
            RestartButton restartButton,
            MainAudio mainAudio)
        {
            _data = data;   
            _restartButton = restartButton;
            _mainAudio = mainAudio; 
        }
        
        private void Awake()
        {
            Mover = GetComponent<PlayerMover>();
            DeathHandler = GetComponent<PlayerDeathHandler>();

            BulletShoot = GetComponent<BulletShoot>();
            Laser = GetComponentInChildren<ShootLaser>();

            _restartButton.OnRestartGame += Restart;
        }

        private void OnDestroy() => 
            _restartButton.OnRestartGame -= Restart;

        public void SetInput(Vector2 input) =>
            Mover.SetInput(input);  
        
        public void TeleportReaction() =>
            _mainAudio.PlaySfx(_data.DashClip);

        private void Restart()
        {
            DeathHandler.ResetState();
            Mover.Stop();
        }
    }
}