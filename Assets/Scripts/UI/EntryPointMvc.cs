using System.Collections;
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

    public class Score : EntryPointMvc
    {
        private HandlerScore _handlerScore;
        private readonly float _counterSpeed = 700;
        private Coroutine _currentCoroutine;
        
        [Inject]
        public void Init(HandlerScore handlerScore)
        {
            _handlerScore = handlerScore;
        }

        protected override void Awake()
        {
            base.Awake();
            _handlerScore.NewTargetScore += Model.SetCount;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handlerScore.NewTargetScore -= Model.SetCount;
        }
        
        private void StartCounterNewChange(int targetScore)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            
            _currentCoroutine = StartCoroutine(CounterNewChange(targetScore));
        }
        
        private IEnumerator CounterNewChange(int targetScore)
        {
            while (Model.Count != targetScore)
            {
                var value = Mathf.RoundToInt(Mathf.MoveTowards(Model.Count, targetScore, _counterSpeed * Time.deltaTime));
                Model.SetCount(value);
                yield return null;
            }
            _currentCoroutine = null;
        }
    }
}

