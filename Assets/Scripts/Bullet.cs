using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [field: SerializeField] 
    public float MoveSpeed { get; private set; } = default;
    private Rigidbody2D _rg;

    private void Awake()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    protected override void OnFixedUpdate()
    { 
        _rg.linearVelocity = transform.up * MoveSpeed;
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
