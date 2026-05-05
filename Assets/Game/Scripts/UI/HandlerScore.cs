using Zenject;
using System;
using Project.Player;

namespace Project.UI
{
    public class HandlerScore : IInitializable, IDisposable
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

        public void Initialize() => 
            _playerController.DeathHandler.OnHitPlayer += ResetCount;

        public void Dispose() =>
            _playerController.DeathHandler.OnHitPlayer -= ResetCount;        


        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
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
