using UnityEngine;
using Zenject;

public class PlayerShoot : ManagedBehaviour
{
    [Inject] private BulletPool _bulletPool;
    [SerializeField] private float cooldown = default;
    [SerializeField] private float instantiateOffset = default;
    private float _lastTime;
    private bool _mayShoot;

    protected override void OnUpdate()
    {
        _lastTime += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && _lastTime >= cooldown)
        {
            Shoot();
            _lastTime = 0f;
        }
    }

    private void Shoot()
    {
        var obj = _bulletPool.Get();
        var spawnPos = (Vector2)transform.position + (Vector2)transform.up * instantiateOffset;
        obj.transform.position = spawnPos;
        obj.transform.rotation = transform.localRotation;
    }
}
