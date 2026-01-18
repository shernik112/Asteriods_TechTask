using UnityEngine;
using Zenject;

public class BulletPool : ObjectPool
{
    [Inject] private GameObject _bulletPrefab;

    public override void ManagedInintialize()
    {
        PoolPrefab = _bulletPrefab;
        base.ManagedInintialize();
    }
}
