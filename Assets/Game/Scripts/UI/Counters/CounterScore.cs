using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class CounterScore :  CounterStarter
    {
        [field: SerializeField] public float CounterSpeed { get; private set; }
        
        private HandlerScore _handlerScore;
        private CancellationTokenSource _counterCts;
        
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
            
            _counterCts?.Cancel();
            _counterCts?.Dispose();
        }
        
        private void StartCounterNewChange(int targetScore)
        {
            _counterCts?.Cancel();
            _counterCts?.Dispose();
            _counterCts = new CancellationTokenSource();
            
            CountNewChange(targetScore, _counterCts.Token).Forget();
        }
        
        private async UniTask CountNewChange(int targetScore, CancellationToken cts)
        {
            while (Model.Count != targetScore)
            {
                var value = Mathf.RoundToInt(Mathf.MoveTowards(Model.Count, targetScore, CounterSpeed * Time.deltaTime));
                Model.SetCount(value);
                await UniTask.NextFrame(cts);
            }
        }
    }
}