using UnityEngine;
using Zenject;

public class AsteroidPool : ObjectPool
{
    [Inject] private GameObject _asteroidPrefab;
    protected override int _startCount => 10;

    public override void ManagedInintialize()
    {
        _poolPrefab = _asteroidPrefab;
        base.ManagedInintialize();
    }

    public override void ReturnToPool(GameObject obj)
    {
        obj.GetComponent<AsteroidBehaviour>().SetDefaultParameters();
        base.ReturnToPool(obj);
    }
}

