using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Zenject;

namespace Project.System.Analytics.Firebase
{
    public class FirebaseAnalyticsService : IAnalyticsService, IInitializable
    {
        private readonly string _eventGameStarted = "game_started";
        private readonly string _eventGameEnded = "game_ended";
        private readonly string _eventLaserUsed = "laser_used";

        private readonly string _paramBulletShots = "bullet_shots";
        private readonly string _paramLaserUsedCount = "laser_used_count";
        private readonly string _paramAsteroidsDestroyed = "asteroids_destroyed";
        private readonly string _paramUfoDestroyed = "ufo_destroyed";

        private bool _isReady;

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            var status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
            _isReady = status == DependencyStatus.Available;
        }

        public void LogGameStarted()
        {
            if (!_isReady)
                return;

            FirebaseAnalytics.LogEvent(_eventGameStarted);
        }

        public void LogGameEnded(int bulletShotsCount, int laserUsedCount, int asteroidsDestroyedCount, int ufoDestroyedCount)
        {
            if (!_isReady)
                return;

            FirebaseAnalytics.LogEvent(_eventGameEnded,
                new Parameter(_paramBulletShots, bulletShotsCount),
                new Parameter(_paramLaserUsedCount, laserUsedCount),
                new Parameter(_paramAsteroidsDestroyed, asteroidsDestroyedCount),
                new Parameter(_paramUfoDestroyed, ufoDestroyedCount));
        }

        public void LogLaserUsed()
        {
            if (!_isReady)
                return;

            FirebaseAnalytics.LogEvent(_eventLaserUsed);
        }
    }
}
