
namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        private void Start() =>
            PlayerController.Laser.NewCountShotLaser += UpdateValue;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PlayerController.Laser.NewCountShotLaser -= UpdateValue;
        }

        private void UpdateValue(int count)
        {
            Count = count;
            Text.text = count.ToString();
        }
    }
}
