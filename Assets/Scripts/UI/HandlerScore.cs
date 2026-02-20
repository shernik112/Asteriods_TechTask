using System.Collections;
using UnityEngine;
using Zenject; 

namespace Project.UI
{
    public class HandlerScore : BaseCounter
    {
        [SerializeField] private float counterSpeed = default;
        
        private FinalScore _finalScore;
        private int _targetScore;

        [Inject]
        public void Construct(FinalScore finalScore)
        {
            _finalScore = finalScore;
        }

        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
            StopAllCoroutines();
            
            _targetScore += countDefeatedEnemy;
            StartCoroutine(CounterNewChange());
        }

        protected override void ResetCount()
        {
            _finalScore.ShowFinalScore(_targetScore);
            _targetScore = default;
            base.ResetCount();
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
