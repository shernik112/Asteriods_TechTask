using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class ButtonView : MonoBehaviour, IButtonView
    {
        public event Action OnButtonClicked;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(HandleClick);    
        
        public void ChangeInteractable(bool isInteractable) =>
            _button.interactable = isInteractable;

        private void HandleClick() =>
            OnButtonClicked?.Invoke();
    }
}