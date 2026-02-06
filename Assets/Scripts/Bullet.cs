using UnityEngine;
using Zenject;

public class Bullet : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [SerializeField] private float moveSpeed = default;
    private Rigidbody2D _rg;

    private void Awake()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    protected override void OnFixedUpdate()
    { 
        _rg.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"{typeof(Bullet)} bullet hit enemy");
        _bulletPool.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{typeof(Bullet)} bullet entered trigger");
        _bulletPool.ReturnToPool(gameObject);
    }
}
