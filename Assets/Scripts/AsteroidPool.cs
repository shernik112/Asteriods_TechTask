using UnityEngine;

public class AsteroidPool : ObjectPool
{
    protected override int StartCount => 5;
    
    public override void ReturnToPool(GameObject obj)
    {
        var asteroid = obj.GetComponent<AsteroidBehaviour>();
        asteroid.SetDefaultParameters();
        base.ReturnToPool(obj);
    }
}

