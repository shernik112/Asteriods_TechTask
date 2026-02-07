
namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        public void UpdateValue(int value) => CountChange(value);
    
        protected override void CountChange(int count)
        {
            Count = count;
            Text.text = count.ToString();
        }
    }
}
