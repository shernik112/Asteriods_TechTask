using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(IndicatorsView))]
    public class IndicatorsEntryPoint : MonoBehaviour
    {
        private PlayerController _playerController;
        private IndicatorsModel _model;
        private IndicatorsController _controller;
        private IndicatorsView _view;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Awake()
        {
            _view = GetComponent<IndicatorsView>();
            _model = new IndicatorsModel();
            _controller = new IndicatorsController(_playerController, _model);
            _model.OnNewText += ShowNewText;
        }

        private void Update() =>
            _controller.Update();

        private void OnDestroy() =>
            _model.OnNewText -= ShowNewText;

        private void ShowNewText() =>
            _view.ShowText(_model.Text);
    }
}
