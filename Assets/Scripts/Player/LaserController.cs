using System;
using Project.System;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class LaserController : Controller<LaserModel>
    {
        [field:SerializeField] public LaserData Data { get; private set; }

        public event Action<int> NewCountShotLaser;
        private LaserView _view;
        private RestartButton _restartButton;
        private MainAudio _mainAudio;

        [Inject]
        public void Construct(
            RestartButton restartButton,
            MainAudio mainAudio)
        {
            _restartButton = restartButton;
            _mainAudio = mainAudio;
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            _view = GetComponent<LaserView>();
            _view.Init(Model);

            Model.OnChangeCount += ShowNewCount;
            _restartButton.OnRestartGame += Restart;
        }

        private void OnDestroy()
        {
            Model.OnChangeCount -= ShowNewCount;
            _restartButton.OnRestartGame -= Restart;
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
            NewCountShotLaser?.Invoke(Model.CurrentCountShotLaser);
            _view.ChangeVisibility(true);
            _view.TurnLaser();
        }
        
        protected override LaserModel CreateModel()
        {
            var model = ScriptableObject.CreateInstance<LaserModel>();
            model.Init(Data);
            return model;
        }
        
        private void Restart() => 
            Model.RestartValues();

        private void ShowNewCount(int count) =>
            NewCountShotLaser?.Invoke(count);
    }
}
