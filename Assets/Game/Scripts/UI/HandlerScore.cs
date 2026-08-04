using Zenject;
using System;
using JetBrains.Annotations;
using Project.Player;

namespace Project.UI
{
    [UsedImplicitly]
    public class HandlerScore : IInitializable, IDisposable
    {
        public event Action<int> FinalScoreReceived;
        public event Action<int> IsRecordScore;
        public event Action<int> NewTargetScore;

        private const string RecordKey = "record";
        private readonly SaveService _saveService = new ();

        private PlayerDeathHandler _deathHandler;
        private int _record;
        private int _targetScore;

        [Inject]
        public void Construct(PlayerDeathHandler deathHandler)
        {
            _deathHandler = deathHandler;
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
