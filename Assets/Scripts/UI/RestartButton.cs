using System;
using UnityEngine;

namespace Project.UI
{
    [RequireComponent(typeof(ButtonView))]
    public class RestartButton : MonoBehaviour
    {
        public event Action OnRestartGame;

        private ButtonView _view;
        
        private void Awake()
        {
            _view = GetComponent<ButtonView>();
            _view.OnClicked += HandleButtonClick;
        }

        private void OnDestroy() =>
            _view.OnClicked -= HandleButtonClick;

        private void HandleButtonClick() =>
            OnRestartGame?.Invoke();
    }
}
