using Zenject;
using System;
using JetBrains.Annotations;
using Project.Player;
using Project.System;

namespace Project.UI
{
    [UsedImplicitly]
    public class HandlerScore : IInitializable, IDisposable
    {
        public event Action<int> FinalScoreReceived;
        public event Action<int> IsRecordScore;
        public event Action<int> NewTargetScore;

        private const string RecordKey = "record";
        private readonly ISaveService _saveService;
        private readonly PlayerDeathHandler _deathHandler;
        
        private int _record;
        private int _targetScore;

        public HandlerScore(PlayerDeathHandler deathHandler, ISaveService saveService)
        {
            _deathHandler = deathHandler;
            _saveService = saveService;
        }

        public void Initialize()
        {
            _record = _saveService.Load(RecordKey);
            _deathHandler.OnHitPlayer += ResetCount;
        }

        public void Dispose() =>
            _deathHandler.OnHitPlayer -= ResetCount;

        public void CountScoreDefeatedEnemy(int countDefeatedEnemy)
        {
            _targetScore += countDefeatedEnemy;
            NewTargetScore?.Invoke(_targetScore);
        }

        private void ResetCount()
        {
            FinalScoreReceived?.Invoke(_targetScore);

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
