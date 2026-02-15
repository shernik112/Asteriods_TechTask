using UnityEngine;
using Zenject; 

namespace Project.UI
{
    public class HandlerScore : BaseCounter
    {
        [SerializeField] private int countAsteroid;
        [SerializeField] private int countUfo;
    
        private FinalScore _finalScore;

        [Inject]
        public void Construct(FinalScore finalScore)
        {
            _finalScore = finalScore;
        }
    
        public void CountDefeatedEnemy(int countDefeatedEnemy) => CountChange(countDefeatedEnemy);

        protected override void ResetCount()
        {
            _finalScore.ShowFinalScore(Count);
            base.ResetCount();
        }

        protected override void CountChange(int countEnemy)
        {
            Count += countEnemy;
            Text.text = Count.ToString();
        }
    }
}
