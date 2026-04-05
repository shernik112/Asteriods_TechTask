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
            _controller = new IndicatorsController(_playerController, _model, _view);
        }

        private void Start() =>
            _controller.Start();
        
        private void OnDestroy() =>
            _controller.OnDestroy();
        
        private void Update() =>
            _controller.Update();
    }
}
