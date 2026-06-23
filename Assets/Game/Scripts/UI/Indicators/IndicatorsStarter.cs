using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(IndicatorsView))]
    public class IndicatorsStarter : MonoBehaviour
    {
        private PlayerMover _mover;
        private ShootLaser _laser;
        private IndicatorsModel _model;
        private IndicatorsPresenter _presenter;
        private IIndicatorsView _view;

        [Inject]
        public void Construct(PlayerMover mover, ShootLaser laser)
        {
            _mover = mover;
            _laser = laser;
        }

        private void Awake()
        {
            _view = GetComponent<IIndicatorsView>();
            _model = new IndicatorsModel(_mover, _laser);
            _presenter = new IndicatorsPresenter(_model, _view);
            _presenter.Awake();
        }
        
        private void OnDestroy() =>
            _presenter.OnDestroy();
        
        private void Update() =>
            _presenter.Update();
    }
}
