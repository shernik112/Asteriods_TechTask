using UnityEngine;
using Zenject;

public class UFOPool : ObjectPool
{
    [Inject] private GameObject _ufoPrefab;
    public override void ManagedInintialize()
    {
        _poolPrefab = _ufoPrefab;
        base.ManagedInintialize();
    }
}
