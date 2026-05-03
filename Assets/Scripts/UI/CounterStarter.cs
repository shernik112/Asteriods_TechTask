    using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(CounterView))]
    public class CounterStarter : MonoBehaviour
    {
        protected PlayerController PlayerController;
        protected CounterModel Model;

        private CounterPresenter _counterPresenter;
        private CounterView _counterView;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        protected virtual void Awake()
        {
            _counterView = GetComponent<CounterView>();
            Model = new CounterModel();
            _counterPresenter = new CounterPresenter(Model, _counterView);
            _counterPresenter.Awake();
            
            PlayerController.OnHitPlayer += ResetCount;
        }

        protected virtual void OnDestroy()
        {
            PlayerController.OnHitPlayer -= ResetCount;
            
            _counterPresenter.OnDestroy();
        }

        private void ResetCount() =>
            Model.Reset();
    }
}

