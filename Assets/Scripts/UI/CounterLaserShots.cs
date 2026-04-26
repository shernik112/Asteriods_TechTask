namespace Project.UI
{
    public class CounterLaserShots : MvpStarter
    {
        protected override void Start()
        {
            base.Start();
            PlayerController.Laser.NewCountShotLaser += Model.SetCount;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PlayerController.Laser.NewCountShotLaser -= Model.SetCount;
        }
    }
}