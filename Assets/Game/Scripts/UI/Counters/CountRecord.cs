using Zenject;

namespace Project.UI
{
    public class CountRecord : NumberStarter
    {
        private HandlerScore _handlerScore;

        [Inject]
        public void Construct(HandlerScore handlerScore)
        {
            _handlerScore = handlerScore;
        }

        protected override void Awake()
        {
            base.Awake();
            _handlerScore.IsRecordScore += ShowRecordScore;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handlerScore.IsRecordScore -= ShowRecordScore;
        }

        private void ShowRecordScore(int count)
        {
            Model.SetCount(count);
        }
    }
}