using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(MvcView))]
    public class EntryPointMvc : MonoBehaviour
    {
        protected PlayerController PlayerController;

        private MvcController _controller;
        protected MvcModel Model;
        private MvcView _view;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        protected virtual void Awake()
        {
            _view = GetComponent<MvcView>();
            PlayerController.OnHitPlayer += ResetCount;
        }

        protected virtual void Start()
        {
            Model = new MvcModel();
            _controller = new MvcController(Model, _view);

            _controller.Start();
        }

        protected virtual void OnDestroy()
        {
            PlayerController.OnHitPlayer -= ResetCount;
            _controller.OnDestroy();
        }

        private void ResetCount() =>
            Model.Reset();
    }
}

