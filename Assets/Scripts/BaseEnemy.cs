using UnityEngine;
using Zenject;

public interface IEnemy
{
    bool IsFirstEnterToTeleport { get ; set; }
}
public abstract class BaseEnemy<TPool> : ManagedBehaviour, IEnemy
where TPool : ObjectPool
{ 
    protected TPool Pool;
    protected abstract int CountScoreByDefeat { get; }
    protected CharacterController ChController { get; private set; }
    protected HandlerScore _handlerScore;
    public bool IsFirstEnterToTeleport { get; set; } = false;
    
    [Inject]
    public void Construct(TPool pool, CharacterController chController, HandlerScore handlerScore)
    {
        Pool = pool;
        ChController = chController;
        _handlerScore = handlerScore;
    }

    private void OnEnable()
    {
        if (ChController != null) ChController.OnHitPlayer += ReturnSelf;
    }

    private void OnDisable()
    {
        if (ChController != null) ChController.OnHitPlayer -= ReturnSelf;
    }

    protected virtual void HitBullet() => Pool.ReturnToPool(gameObject);
    protected virtual void HitLaser() => Pool.ReturnToPool(gameObject);

    private void ReturnSelf()
    {
        Debug.Log($"{typeof(BaseEnemy<>)} ReturnSelf ");
        Pool.ReturnToPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Bullet>(out var playerBullet)) HitBullet();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{typeof(BaseEnemy<>)} Laser Trigger");
        if (other.TryGetComponent<ShootLaser>( out var laser)) HitLaser();
    }
}
