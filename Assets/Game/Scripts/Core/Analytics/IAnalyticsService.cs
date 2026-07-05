namespace Project.System.Analytics
{
    public interface IAnalyticsService
    {
        void LogGameStarted();
        void LogGameEnded(int bulletShotsCount, int laserUsedCount, int asteroidsDestroyedCount, int ufoDestroyedCount);
        void LogLaserUsed();
    }
}
