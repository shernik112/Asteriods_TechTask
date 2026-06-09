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
            _handlerScore.IsFinalScore += ShowFinalScore;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handlerScore.IsFinalScore -= ShowFinalScore;
        }

        private void ShowFinalScore(int count)
        {
            Model.SetCount(count);
        }
    }
}