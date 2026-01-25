using UnityEngine;
using Zenject;

public class Bullet : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [SerializeField] private float moveSpeed = default;
    private Rigidbody2D _rg;
    public override void ManagedInintialize()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    protected override void ManagedFixedUpdate()
    { 
        _rg.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"{typeof(Bullet)} bullet hit enemy");
        _bulletPool.Return(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{typeof(Bullet)} bullet entered trigger");
        _bulletPool.Return(gameObject);
    }
}
