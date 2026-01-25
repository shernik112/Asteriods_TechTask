using UnityEngine;
using Zenject;

public class UFOBehaviour : BaseEnemy
{
    [Inject] private CharacterController _chController;
    [Inject] private UFOPool _ufoPool;
    [SerializeField] private float speed = default;
    protected override void ManagedUpdate()
    {
        Vector2 posPlayer = _chController.gameObject.transform.position;
        var targetDir = posPlayer - (Vector2)transform.position;
        targetDir.Normalize();
        transform.Translate(targetDir * speed * Time.deltaTime, Space.World);
    }

    protected override void HitBullet()
    {
        _ufoPool.Return(gameObject);
    }

    protected override void HitLaser()
    {
        _ufoPool.Return(gameObject);
    }
}
