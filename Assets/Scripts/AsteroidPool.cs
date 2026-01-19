using UnityEngine;
using Zenject;

public class AsteroidPool : ObjectPool
{
    [Inject] private GameObject _asteroidPrefab;

    public override void ManagedInintialize()
    {
        _poolPrefab = _asteroidPrefab;
        base.ManagedInintialize();
    }
}

