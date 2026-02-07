using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ManagedBehaviour
{
    protected virtual int StartCount => 5;
    
    private PrefabInstaller _installer;
    private Queue<GameObject> _pool = new Queue<GameObject>();
    
    protected GameObject PoolPrefab { get; set; }  

    protected virtual void Awake()
    {
        _installer = FindFirstObjectByType<PrefabInstaller>();
    }

    private void Start()
    {
        for (var i = 0; i < StartCount; i++)
            CreateObject();
    }

    private void CreateObject()
    {
        var obj = Instantiate(PoolPrefab, transform);
        if(_installer != null)  _installer.InjectGo(obj);
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    public GameObject Get()
    {
        if(_pool.Count == 0)
            CreateObject();
        var obj = _pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public virtual void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
