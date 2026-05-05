using System;

namespace Project.UI
{
    public interface IButtonView
    {
        event Action OnButtonClicked;
        
        public void ChangeInteractable(bool isInteractable);
    }
}