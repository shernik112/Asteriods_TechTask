using System;
using Project.System;
using UnityEngine;
using Zenject; 

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameObject laserPrefab;

        private EventBus _eventBus;
        private MainAudio _mainAudio;
        private PauseHandler _pauseHandler;
        private PlayerModel _model;
        
        public PlayerView View { get; private set; }
        public LaserController Laser { get; private set; }
        public BulletShoot BulletShoot { get; private set; }
        public Rigidbody2D Rb { get; private set; }

        [Inject]
        public void Construct(
            EventBus eventBus, 
            PauseHandler pauseHandler, 
            MainAudio mainAudio)
        {
            _eventBus = eventBus;
            _pauseHandler = pauseHandler;
            _mainAudio = mainAudio;
        }

        private void Awake()
        {
            _model = new PlayerModel(playerData);
            
            View = transform.parent.GetComponentInChildren<PlayerView>();
            View.Init(_model, _eventBus, _mainAudio, _pauseHandler);
            Rb = View.gameObject.GetComponent<Rigidbody2D>();
            BulletShoot = View.GetComponent<BulletShoot>();
            Laser = transform.parent.GetComponentInChildren<LaserController>();
            
            _eventBus.OnRestartGame += OnRestart;
        }

        private void Start()
        {
            Debug.Log($"Player {Laser == null}");
        }

        private void OnDestroy() =>
            _eventBus.OnRestartGame -= OnRestart;
        
        private void OnRestart()
        {
            _model.ResetState();
            View.ResetState();
        }
        
        public void SetInput(Vector2 input) => 
            _model.SetInput(input);
    }
}
