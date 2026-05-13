using Zenject;
using TMPro;

namespace Project.UI
{
    public class FinalScore : NumberStarter
    {
        private TMP_Text _text;
        private HandlerScore _handlerScore;

        [Inject]
        public void Construct(HandlerScore handlerScore)
        {
            _handlerScore = handlerScore;
        }
        
        protected override void Awake()
        {
            _handlerScore.IsFinalScore += ShowFinalScore;
        }

        protected override void OnDestroy()
        {
            _handlerScore.IsFinalScore -= ShowFinalScore;
        }

        private void ShowFinalScore(int count)
        {
            Model.SetCount(count);
        }
    }
}