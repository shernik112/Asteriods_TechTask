using System;

namespace Project.UI
{
    public class ButtonPresenter
    {
        public event Action OnClicked;
        
        private readonly ButtonModel _model;
        private readonly IButtonView _view;
        
        public ButtonPresenter(ButtonModel model, IButtonView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize() 
        {
            _model.OnChangeInteractable += _view.ChangeInteractable;
            _view.OnButtonClicked += HandleClick;
            
            _model.SetInteractable(true);
        }

        public void OnDestroy()
        {
            _model.OnChangeInteractable += _view.ChangeInteractable;
            _view.OnButtonClicked -= HandleClick;
        }
        
        private void HandleClick() =>
            OnClicked?.Invoke();
    }
}