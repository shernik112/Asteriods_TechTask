using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class LaserController : MonoBehaviour
    {
        [field:SerializeField] public LaserData Data { get; private set; }
        
        public LaserModel Model { get; private set; }
        private LaserView _view;
        private EventBus _eventBus;
        private MainAudio _mainAudio;

        [Inject]
        public void Construct(
            EventBus eventBus,
            MainAudio mainAudio)
        {
            _eventBus = eventBus;
            _mainAudio = mainAudio;
        }
        
        private void Awake()
        {
            Model = new LaserModel(Data);
            _view = GetComponent<LaserView>();
            _view.Init(Model);

            Model.OnChangeCount += ShowNewCount;
            _eventBus.OnRestartGame += Restart;
        }

        private void OnDestroy()
        {
            Model.OnChangeCount -= ShowNewCount;
            _eventBus.OnRestartGame -= Restart;
        }
        
        private void Start()
        {
            _view.ChangeVisibility(false);
            Model.Start();
        }

        private void Update()
        {
            Model.Update();
            
            if (!(Model.LastRechargeTime >= Data.RechargeDuration && Model.CurrentCountShotLaser < Data.DefaultCountShotLaser))
                return;
            
            Model.IncreaseCountShotLaser();
            _mainAudio.PlaySfx(Data.newChargeClip);
        }
        
        public void TryShoot()
        {
            if (!(Model.LastShootTime >= Data.CooldownDuration && Model.CurrentCountShotLaser > 0))
                return;
            
            Model.HandleShoot();
            _mainAudio.PlaySfx(Data.shootLaser);
            _eventBus.NewCountShotLaser?.Invoke(Model.CurrentCountShotLaser);
            _view.ChangeVisibility(true);
            _view.TurnLaser();
        }
        
        private void Restart() => 
            Model.RestartValues();

        private void ShowNewCount(int count) =>
            _eventBus.NewCountShotLaser?.Invoke(count);
    }
}
