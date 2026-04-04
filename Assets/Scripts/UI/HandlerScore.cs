using Project.System;
using UnityEngine;
using Zenject;
using System;
using Project.Player;

namespace Project.UI
{
    public class HandlerScore : MonoBehaviour
    {
        public event Action<int> IsFinalScore;
        public event Action<int> NewTargetScore;
        private PlayerController _playerController;
        private int _targetScore;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Awake() => 
            _playerController.OnHitPlayer += ResetCount;

        private void OnDestroy() =>
            _playerController.OnHitPlayer -= ResetCount;        


        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
            StopAllCoroutines();
            
            _targetScore += countDefeatedEnemy;

            NewTargetScore?.Invoke(_targetScore);
        }

        private void ResetCount()
        {
            IsFinalScore?.Invoke(_targetScore);
            _targetScore = default;
        }
    }
}
