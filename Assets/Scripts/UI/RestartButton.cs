using System;
using UnityEngine;

namespace Project.UI
{
    [RequireComponent(typeof(ButtonView))]
    public class RestartButton : MonoBehaviour
    {
        public event Action OnRestartGame;

        private ButtonPresenter _presenter;
        private ButtonModel _model;
        private IButtonView _view;

        private void Awake()
        {
            _view = GetComponent<ButtonView>();

            _model = new ButtonModel();
            _presenter = new ButtonPresenter(_model, _view);

            _presenter.OnClicked += HandleButtonClick;
        }

        private void Start() =>
            _presenter.Initialize();

        private void OnDestroy()
        {
            _presenter.OnClicked -= HandleButtonClick;
            _presenter.OnDestroy();
        }

        private void HandleButtonClick() =>
            OnRestartGame?.Invoke();
    }
}
