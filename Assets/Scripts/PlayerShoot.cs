using UnityEngine;
using Zenject;

public class PlayerShoot : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [SerializeField] private float cooldown = default;
    [SerializeField] private float instantiateOffset = default;
    private float _lastTime;
    private bool _mayShoot;
    private SpriteRenderer _spriteRenderer;
    public override void ManagedInintialize() => _spriteRenderer = GetComponent<SpriteRenderer>();

    protected override void ManagedUpdate()
    {
        _lastTime += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && _lastTime >= cooldown && _spriteRenderer.enabled)
        {
            Shoot();
            _lastTime = 0f;
        }
    }

    private void Shoot()
    {
        var obj = _bulletPool.Get();
        Vector2 spawnPos = (Vector2)transform.position + (Vector2)transform.up * instantiateOffset;
        obj.transform.position = spawnPos;
        obj.transform.rotation = transform.localRotation;
    }
}
