using System;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class HandlerScore : MonoBehaviour
    {
        public event Action<int> NewTargetScore;
        public event Action<int> IsFinalScore;
        
        private PlayerController _playerController;
        private int _targetScore;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        private void Start() =>
            _playerController.View.OnHitPlayer += ResetCount;

        private void OnDestroy() =>
            _playerController.View.OnHitPlayer -= ResetCount;        


        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
            StopAllCoroutines();
            _targetScore += countDefeatedEnemy;

            NewTargetScore?.Invoke(_targetScore);
        }

        private void ResetCount()
        {
            IsFinalScore?.Invoke(_targetScore);
            _targetScore = 0;
        }
    }
}
