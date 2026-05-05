using System.Collections;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class CounterScore :  CounterStarter
    {
        [field: SerializeField] public float CounterSpeed { get; private set; }
        
        private HandlerScore _handlerScore;
        private Coroutine _currentCoroutine;
        
        [Inject]
        public void Init(HandlerScore handlerScore) =>
            _handlerScore = handlerScore;

        protected override void Awake()
        {
            base.Awake();
            _handlerScore.NewTargetScore += StartCounterNewChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handlerScore.NewTargetScore -= StartCounterNewChange;
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
                var value = Mathf.RoundToInt(Mathf.MoveTowards(Model.Count, targetScore, CounterSpeed * Time.deltaTime));
                Model.SetCount(value);
                yield return null;
            }
            _currentCoroutine = null;
        }
    }
}