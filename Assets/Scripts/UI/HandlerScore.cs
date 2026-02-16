using System.Collections;
using UnityEngine;
using Zenject; 

namespace Project.UI
{
    public class HandlerScore : BaseCounter
    {
        [SerializeField] private int countAsteroid = default;
        [SerializeField] private int countUfo = default;
        [SerializeField] private float counterSpeed = default;
        
        private FinalScore _finalScore;
        private int _targetScore;

        [Inject]
        public void Construct(FinalScore finalScore)
        {
            _finalScore = finalScore;
        }
    
        public void CountDefeatedEnemy(int countDefeatedEnemy) => CountChange(countDefeatedEnemy);

        protected override void ResetCount()
        {
            _finalScore.ShowFinalScore(Count);
            _targetScore = default;
            base.ResetCount();
        }

        protected override void CountChange(int countEnemy)
        {
            StopAllCoroutines();
            
            _targetScore += countEnemy;
            StartCoroutine(CounterNewChange());
        }

        private IEnumerator CounterNewChange()
        {
            while (Count != _targetScore)
            {
                Count = Mathf.RoundToInt(Mathf.MoveTowards(Count, _targetScore, counterSpeed * Time.deltaTime));
                Text.text = Count.ToString();
                yield return null;
            }
        }
    }
}
