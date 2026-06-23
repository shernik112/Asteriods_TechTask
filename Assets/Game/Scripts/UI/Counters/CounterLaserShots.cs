using Project.Player;
using Zenject;

namespace Project.UI
{
    public class CounterLaserShots : CounterStarter
    {
        private ShootLaser _laser;

        [Inject]
        public void ConstructLaser(ShootLaser laser)
        {
            _laser = laser;
        }

        private void Start() =>
            _laser.NewCountShotLaser += Model.SetCount;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _laser.NewCountShotLaser -= Model.SetCount;
        }
    }
}
