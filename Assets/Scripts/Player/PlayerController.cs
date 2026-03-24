using Project.System;
using Project.UI;
using UnityEngine;
using Zenject; 

namespace Project.Player
{
    public class PlayerController : Controller<PlayerModel>
    {
        public PlayerView View { get; private set; }
        public LaserController Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        
        [SerializeField] private PlayerData playerData;

        private RestartButton _restartButton;
        private MainAudio _mainAudio;
        private PauseHandler _pauseHandler;
        
        [Inject]
        public void Construct(
            RestartButton restartButton, 
            PauseHandler pauseHandler, 
            MainAudio mainAudio)
        {
            _restartButton = restartButton;
            _pauseHandler = pauseHandler;
            _mainAudio = mainAudio;
        }

        protected override void Awake()
        {
            base.Awake();
            
            View = transform.GetComponent<PlayerView>();
            View.Init(Model, _mainAudio, _pauseHandler);
            Rb = View.gameObject.GetComponent<Rigidbody2D>();
            BulletShoot = View.GetComponent<BulletShoot>();
            Laser = transform.GetComponentInChildren<LaserController>();
            
            _restartButton.OnRestartGame += OnRestart;
        }
        
        private void OnDestroy() =>
            _restartButton.OnRestartGame -= OnRestart;
        
        public void SetInput(Vector2 input) => 
            Model.SetInput(input);
        
        protected override PlayerModel CreateModel()
        {
            var model = ScriptableObject.CreateInstance<PlayerModel>();
            model.Init(playerData);
            return model;
        }
        
        private void OnRestart()
        {
            Model.ResetState();
            View.ResetState();
        }
    }
}
