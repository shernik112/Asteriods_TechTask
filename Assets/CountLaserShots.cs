
public class CountLaserShots : BaseCounter
{
    public void UpdateValue(int value) => AddToCount(value);
    
    protected override void AddToCount(int count)
    {
        Count = count;
        Text.text = count.ToString();
    }
}
