
using UnityEngine;

public class AsteroidBehaviour : BaseEnemy
{
    [SerializeField] private float multiplierBoost;
    [SerializeField] private float moveSpeed;
    private float _currentSpeed;
    private int _sizeLevel = 1;
    public override void InitParams(int size)
    {
        _currentSpeed = moveSpeed * multiplierBoost * size;
    }

    protected override void HitBullet()
    {
        
    }

    protected override void HitLaser()
    {
        
    }
}
