    using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(MvpView))]
    public class MvpStarter : MonoBehaviour
    {
        protected PlayerController PlayerController;
        protected MvcModel Model;

        private MvcController _controller;
        private MvpView _view;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        protected virtual void Awake()
        {
            _view = GetComponent<MvpView>();
            Model = new MvcModel();
            _controller = new MvcController(Model, _view);
            PlayerController.OnHitPlayer += ResetCount;
        }

        protected virtual void Start() =>
            _controller.Start();

        protected virtual void OnDestroy()
        {
            PlayerController.OnHitPlayer -= ResetCount;
            _controller.OnDestroy();
        }

        private void ResetCount() =>
            Model.Reset();
    }
}

