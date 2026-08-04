using Zenject;

namespace Project.UI
{
    public class FinalScore : NumberStarter
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
            _handlerScore.FinalScoreReceived += ShowFinalScoreReceived;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handlerScore.FinalScoreReceived -= ShowFinalScoreReceived;
        }

        private void ShowFinalScoreReceived(int count)
        {
            Model.SetCount(count);
        }
    }
}