
namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        protected override void Awake()
        {
            base.Awake();
            EventBus.NewCountShotLaser += UpdateValue;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventBus.NewCountShotLaser -= UpdateValue;
        }

        private void UpdateValue(int count)
        {
            Count = count;
            Text.text = count.ToString();
        }
    }
}
