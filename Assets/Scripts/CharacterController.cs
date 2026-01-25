using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : ManagedBehaviour
{
    [Inject] private EnemiesSpawner _astroSpawn;
    [SerializeField] private float moveSpeed = default;
    [SerializeField] private float speedAcceleration = default;
    [SerializeField] private float rotateSpeed = default;
    [SerializeField] private float rotateAcceleration = default;
    private Vector2 _input;
    private Rigidbody2D _rg;
    public event Action OnHitPlayer;
    
    public override void ManagedInintialize()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    protected override void ManagedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E)) _astroSpawn.CreateAsteroid(1);
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
        _input.Normalize(); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            OnHitPlayer?.Invoke();
    }

    protected override void  ManagedFixedUpdate()
    {
        _rg.angularVelocity = Mathf.MoveTowards(_rg.angularVelocity, -_input.x * rotateSpeed, rotateAcceleration * Time.fixedDeltaTime);
        Vector2 targetVelocity = transform.up * _input.y * moveSpeed;
        _rg.linearVelocity = Vector2.MoveTowards(_rg.linearVelocity, targetVelocity, speedAcceleration * Time.fixedDeltaTime);
    }
}
