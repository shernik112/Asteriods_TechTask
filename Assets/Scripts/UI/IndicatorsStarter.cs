using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(IndicatorsView))]
    public class IndicatorsStarter : MonoBehaviour
    {
        private PlayerController _playerController;
        private IndicatorsModel _model;
        private IndicatorsPresenter _presenter;
        private IIndicatorsView _view;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Awake()
        {
            _view = GetComponent<IIndicatorsView>();
            _model = new IndicatorsModel();
            _presenter = new IndicatorsPresenter(_playerController, _model, _view);
            _presenter.Awake();
        }
        
        private void OnDestroy() =>
            _presenter.OnDestroy();
        
        private void Update() =>
            _presenter.Update();
    }
}
