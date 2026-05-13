using UnityEngine;

namespace Project.UI
{
    [RequireComponent(typeof(CounterView))]
    public class NumberStarter : MonoBehaviour
    {
        protected CounterModel Model;
        private CounterPresenter _counterPresenter;
        private CounterView _counterView;
        
        protected virtual void Awake()
        {
            _counterView = GetComponent<CounterView>();
            Model = new CounterModel();
            _counterPresenter = new CounterPresenter(Model, _counterView);
            _counterPresenter.Awake();
        }

        protected virtual void OnDestroy()
        {
            _counterPresenter.OnDestroy();
        }
    }
}