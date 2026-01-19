
using UnityEngine;
using Zenject;

public class AsteroidBehaviour : BaseEnemy
{
    [Inject] private AsteroidPool _pool;
    public float multiplierBoost;
    public float divideScale;
    private MoveForward _moveForward;
    private int _sizeLevel = 1;
    public override void ManagedInintialize()
    {
        _moveForward = GetComponent<MoveForward>();
    }

    public void InitParams(int size)
    {
        _moveForward.speed = _moveForward.speed * multiplierBoost * size;
        transform.localScale /= divideScale * size;
    }

    protected override void HitBullet()
    {
        if(_sizeLevel == 3) 
            _pool.Return(gameObject);
            
    }

    protected override void HitLaser()
    {
        
    }
}
