using Zenject;
using System;
using Project.Player;

namespace Project.UI
{
    public class HandlerScore : IInitializable, IDisposable
    {
        public event Action<int> IsFinalScore;
        public event Action<int> IsRecordScore;
        public event Action<int> NewTargetScore;

        private const string RecordKey = "record";
        private readonly SaveService _saveService = new ();

        private PlayerController _playerController;
        private int _record;
        private int _targetScore;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Initialize()
        {
            _record = _saveService.Load(RecordKey);
            _playerController.DeathHandler.OnHitPlayer += ResetCount;
        }

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

            if (_targetScore > _record)
            {
                _record = _targetScore;
                _saveService.Save(RecordKey, _record);
            }

            IsRecordScore?.Invoke(_record);
            _targetScore = 0;
            NewTargetScore?.Invoke(_targetScore);
        }
    }
}
