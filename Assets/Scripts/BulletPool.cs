using UnityEngine;
using Zenject;

public class BulletPool : ObjectPool
{
    [Inject] private GameObject _bulletPrefab;

    protected override void Awake()
    {
        PoolPrefab = _bulletPrefab;
        base.Awake();
    }
}
