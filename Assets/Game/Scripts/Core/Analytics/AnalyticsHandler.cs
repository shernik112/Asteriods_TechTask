using System;
using Project.Player;
using Project.UI;
using Zenject;

namespace Project.System.Analytics
{
    public class AnalyticsHandler : IInitializable, IDisposable
    {
        private IAnalyticsService _analyticsService;
        private PlayerDeathHandler _deathHandler;
        private BulletShoot _bulletShoot;
        private ShootLaser _laser;
        private RestartButton _restartButton;

        private int _bulletShotsCount;
        private int _laserUsedCount;
        private int _asteroidsDestroyedCount;
        private int _ufoDestroyedCount;

        [Inject]
        public void Construct(
            IAnalyticsService analyticsService,
            PlayerDeathHandler deathHandler,
            BulletShoot bulletShoot,
            ShootLaser laser,
            RestartButton restartButton)
        {
            _analyticsService = analyticsService;
            _deathHandler = deathHandler;
            _bulletShoot = bulletShoot;
            _laser = laser;
            _restartButton = restartButton;
        }

        public void Initialize()
        {
            _bulletShoot.OnShoot += HandleBulletShot;
            _laser.OnLaserUsed += HandleLaserUsed;
            _deathHandler.OnHitPlayer += HandleGameEnded;
            _restartButton.OnRestartGame += HandleGameStarted;

            _analyticsService.LogGameStarted();
        }

        public void Dispose()
        {
            _bulletShoot.OnShoot -= HandleBulletShot;
            _laser.OnLaserUsed -= HandleLaserUsed;
            _deathHandler.OnHitPlayer -= HandleGameEnded;
            _restartButton.OnRestartGame -= HandleGameStarted;
        }

        public void RegisterAsteroidDestroyed() =>
            _asteroidsDestroyedCount++;

        public void RegisterUfoDestroyed() =>
            _ufoDestroyedCount++;

        private void HandleBulletShot() =>
            _bulletShotsCount++;

        private void HandleLaserUsed()
        {
            _laserUsedCount++;
            _analyticsService.LogLaserUsed();
        }

        private void HandleGameEnded()
        {
            _analyticsService.LogGameEnded(_bulletShotsCount, _laserUsedCount, _asteroidsDestroyedCount, _ufoDestroyedCount);
            ResetCounters();
        }

        private void HandleGameStarted()
        {
            ResetCounters();
            _analyticsService.LogGameStarted();
        }

        private void ResetCounters()
        {
            _bulletShotsCount = 0;
            _laserUsedCount = 0;
            _asteroidsDestroyedCount = 0;
            _ufoDestroyedCount = 0;
        }
    }
}
