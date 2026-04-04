
namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        protected override void Awake()
        {
            base.Awake();
            PlayerController.Laser.NewCountShotLaser += UpdateValue;
        }

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
