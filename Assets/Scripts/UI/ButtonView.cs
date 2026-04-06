using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class ButtonView : MonoBehaviour
    {
        public event Action OnClicked;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(HandleClick);    

        private void HandleClick() =>
            OnClicked?.Invoke();
    }
}