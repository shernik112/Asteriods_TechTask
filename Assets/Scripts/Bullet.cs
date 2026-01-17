using System;
using UnityEngine;

public class Bullet : ManagedBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rg;
    public override void ManagedInintialize()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    public override void ManagedFixedUpdate()
    {
            _rg.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // if () 
    }
}
