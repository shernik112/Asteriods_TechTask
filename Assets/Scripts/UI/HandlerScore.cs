using Project.System;
using UnityEngine;
using Zenject;
using System;

namespace Project.UI
{
    public class HandlerScore : MonoBehaviour
    {

        private EventBus _eventBus;
        private int _targetScore;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake() => 
            _eventBus.OnHitPlayer += ResetCount;

        private void OnDestroy() =>
            _eventBus.OnHitPlayer -= ResetCount;


        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
            StopAllCoroutines();
            
            _targetScore += countDefeatedEnemy;

            _eventBus.NewTargetScore?.Invoke(_targetScore);
        }

        private void ResetCount()
        {
            _eventBus.IsFinalScore?.Invoke(_targetScore);
            _targetScore = default;
        }
    }
}
