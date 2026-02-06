using UnityEngine;
using Zenject;

public class UfoPool : ObjectPool
{
    [Inject] private GameObject _ufoPrefab;
    protected override void Awake()
    {
        PoolPrefab = _ufoPrefab;
        base.Awake();
    }
}
