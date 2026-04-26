namespace Project.UI
{
    public class MvcController
    {
        private MvcModel _model;
        private MvpView _view;

        public MvcController(MvcModel model, MvpView view)
        {
            _model = model;
            _view = view;
        }

        public void Start() =>
            _model.OnNewCount += NewCount;

        public void OnDestroy() =>
            _model.OnNewCount -= NewCount;

        private void NewCount() =>
            _view.UpdateCounter(_model.Count);
    }
}