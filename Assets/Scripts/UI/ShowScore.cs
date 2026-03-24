using System.Collections;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class ShowScore : BaseCounter
    {
        private readonly float _counterSpeed = 700;
        private HandlerScore _handlerScore;
        private Coroutine _currentCoroutine;

        [Inject]
        public void Construct(
            PlayerController playerController,
            HandlerScore handlerScore)
        {
            base.Construct(playerController);
            _handlerScore = handlerScore;
        }

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
            while (Count != targetScore)
            {
                Count = Mathf.RoundToInt(Mathf.MoveTowards(Count, targetScore, _counterSpeed * Time.deltaTime));
                Text.text = Count.ToString();
                yield return null;
            }

            _currentCoroutine = null;
        }
    }
}
