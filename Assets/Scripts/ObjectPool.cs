using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectPool : ManagedBehaviour
{
    private PrefabInstaller _installer;
    private int _startCount = 5;
    protected GameObject _poolPrefab;
    private Queue<GameObject> _pool = new Queue<GameObject>();

    public override void ManagedInintialize()
    {
        _installer = FindFirstObjectByType<PrefabInstaller>();
        for (var i = 0; i < _startCount; i++)
            CreateObject();
    }

    private void CreateObject()
    {
        var obj = Instantiate(_poolPrefab, transform);
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

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
