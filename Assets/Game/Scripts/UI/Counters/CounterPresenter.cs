namespace Project.UI
{
    public class CounterPresenter
    {
        private readonly CounterModel _model;
        private readonly CounterView _counterView;

        public CounterPresenter(
            CounterModel model, 
            CounterView counterView)
        {
            _model = model;
            _counterView = counterView;
        }

        public void Awake() =>
            _model.OnNewCount += NewCount;

        public void OnDestroy() =>
            _model.OnNewCount -= NewCount;

        private void NewCount() =>
            _counterView.UpdateCounter(_model.Count);
    }
}