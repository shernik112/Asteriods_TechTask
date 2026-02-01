using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class CharacterController : ManagedBehaviour
{
    [SerializeField] private float moveSpeed = default;
    [SerializeField] private float speedAcceleration = default;
    [SerializeField] private float rotateSpeed = default;
    [SerializeField] private float rotateAcceleration = default;
    [Inject] private RestartInvoke _restartInvoke;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _input;
    private Rigidbody2D _rb;
    public event Action OnHitPlayer;

    private void OnEnable() => _restartInvoke.OnRestartGame += SetActive;

    private void OnDisable() => _restartInvoke.OnRestartGame -= SetActive;

    public override void ManagedInintialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ManagedUpdate()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
        _input.Normalize(); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"{typeof(CharacterController)} OnCollisionEnter");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"Invoke OnHitPlayer; subscribers = {(OnHitPlayer == null ? 0 : OnHitPlayer.GetInvocationList().Length)}");
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = default;
            OnHitPlayer?.Invoke();
            SetDefaultValues();
        } 
    }

    protected override void  ManagedFixedUpdate()
    {
        _rb.angularVelocity = Mathf.MoveTowards(_rb.angularVelocity, -_input.x * rotateSpeed, rotateAcceleration * Time.fixedDeltaTime);
        Vector2 targetVelocity = transform.up * _input.y * moveSpeed;
        _rb.linearVelocity = Vector2.MoveTowards(_rb.linearVelocity, targetVelocity, speedAcceleration * Time.fixedDeltaTime);
    }

    private void SetDefaultValues()
    {
        _spriteRenderer.enabled = false;
        transform.position = new Vector2(0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SetActive() => _spriteRenderer.enabled = true;
}
