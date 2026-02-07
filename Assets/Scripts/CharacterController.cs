using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class CharacterController : ManagedBehaviour
{ 
    public event Action OnHitPlayer;
    
    [SerializeField] private float moveSpeed = default;
    [SerializeField] private float speedAcceleration = default;
    [SerializeField] private float rotateSpeed = default;
    [SerializeField] private float rotateAcceleration = default;
    
    private RestartInvoke _restartInvoke;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _input;
    
    public ShootLaser Laser { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    [Inject]
    public void Construct(RestartInvoke restartInvoke)
    {
        _restartInvoke = restartInvoke;
    }
    
    private void Awake()
    {
        Laser = GetComponentInChildren<ShootLaser>(true);
        Rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _restartInvoke.OnRestartGame += SetActive;
    }
    
    private void OnDestroy() => _restartInvoke.OnRestartGame -= SetActive;

    protected override void OnUpdate()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
        _input.Normalize(); 
    }
    
    protected override void  OnFixedUpdate()
    {
        Rb.angularVelocity = Mathf.MoveTowards(Rb.angularVelocity, -_input.x * rotateSpeed, rotateAcceleration * Time.fixedDeltaTime);
        Vector2 targetVelocity = transform.up * _input.y * moveSpeed;
        Rb.linearVelocity = Vector2.MoveTowards(Rb.linearVelocity, targetVelocity, speedAcceleration * Time.fixedDeltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"{typeof(CharacterController)} OnCollisionEnter");
        if (other.gameObject.TryGetComponent<IEnemy>(out var enemy))
        {
            Debug.Log($"Invoke OnHitPlayer; subscribers = {(OnHitPlayer == null ? 0 : OnHitPlayer.GetInvocationList().Length)}");
            Rb.linearVelocity = Vector2.zero;
            Rb.angularVelocity = default;
            OnHitPlayer?.Invoke();
            SetDefaultValues();
        } 
    }

    private void SetDefaultValues()
    {
        _spriteRenderer.enabled = false;
        transform.position = new Vector2(0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SetActive() => _spriteRenderer.enabled = true;
}
