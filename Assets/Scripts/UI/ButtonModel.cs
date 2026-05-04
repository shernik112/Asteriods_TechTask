
using System;

namespace Project.UI
{
    public class ButtonModel
    {
        public event Action<bool> OnChangeInteractable;
        
        private bool _isInteractable;
        
        public ButtonModel(bool isInteractable)
        {
            _isInteractable = isInteractable;
        }

        public void ChangeInteractable()
        {
            _isInteractable = !_isInteractable;
            OnChangeInteractable?.Invoke(_isInteractable);
        } 
    }
}