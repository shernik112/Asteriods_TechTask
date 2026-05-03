namespace Project.UI
{
    public class CounterLaserShots : CounterStarter
    {
        private void Start() =>
            PlayerController.Laser.NewCountShotLaser += Model.SetCount;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PlayerController.Laser.NewCountShotLaser -= Model.SetCount;
        }
    }
}