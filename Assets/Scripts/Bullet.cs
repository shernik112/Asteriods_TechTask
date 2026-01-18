using System;
using UnityEngine;
using Zenject;

public class Bullet : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rg;
    public override void ManagedInintialize()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    protected override void ManagedFixedUpdate()
    { 
        _rg.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{typeof(Bullet)} bullet entered trigger");
        _bulletPool.Return(gameObject);
    }
}
