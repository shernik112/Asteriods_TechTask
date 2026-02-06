using UnityEngine;
using Zenject;

public class AsteroidPool : ObjectPool
{
    [Inject] private GameObject _asteroidPrefab;
    protected override int StartCount => 5;

    protected override void Awake()
    {
        PoolPrefab = _asteroidPrefab;
        base.Awake();
    }

    public override void ReturnToPool(GameObject obj)
    {
        var asteroid = obj.GetComponent<AsteroidBehaviour>();
        asteroid.SetDefaultParameters();
        base.ReturnToPool(obj);
    }
}

