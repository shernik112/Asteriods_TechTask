
using System;

namespace Project.UI
{
    public class ButtonModel
    {
        public event Action<bool> OnChangeInteractable;
        
        private bool _isInteractable;

        public void SetInteractable(bool interactable)
        {
            _isInteractable = interactable;
            OnChangeInteractable?.Invoke(_isInteractable);
        } 
    }
}