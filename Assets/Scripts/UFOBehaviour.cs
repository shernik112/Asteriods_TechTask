using UnityEngine;
using Zenject;

public class UFOBehaviour : BaseEnemy<UFOPool>
{
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
        
        Pool.Return(gameObject);
    }

    protected override void HitLaser()
    {
        Pool.Return(gameObject);
    }
}
