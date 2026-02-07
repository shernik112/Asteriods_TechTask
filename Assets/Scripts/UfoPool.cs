using UnityEngine;
using Zenject;

public class UfoPool : ObjectPool
{
    private GameObject _ufoPrefab;
    
    [Inject]
    public void Construct(GameObject ufoPrefab)
    {
        _ufoPrefab = ufoPrefab;
    }
    
    protected override void Awake()
    {
        PoolPrefab = _ufoPrefab;
        base.Awake();
    }
}
