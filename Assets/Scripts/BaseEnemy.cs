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
    private HandlerScore _handlerScore;
    
    protected CharacterController CharacterController { get; private set; }
    public bool IsFirstEnterToTeleport { get; set; } = false;
    public abstract int CountScoreByDefeat { get; set; }
    
    [Inject]
    public void Construct(TPool pool, CharacterController characterController, HandlerScore handlerScore)
    {
        Pool = pool;
        CharacterController = characterController;
        _handlerScore = handlerScore;
    }

    private void OnEnable()
    {
        if (CharacterController != null) CharacterController.OnHitPlayer += ReturnSelf;
    }

    private void OnDisable()
    {
        if (CharacterController != null) CharacterController.OnHitPlayer -= ReturnSelf;
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
        if (other.gameObject.TryGetComponent<Bullet>(out var playerBullet))
        {
            _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
            HitBullet();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ShootLaser>(out var laser))
        {
            _handlerScore.CountDefeatedEnemy(CountScoreByDefeat);
            HitLaser();
        }
        Debug.Log($"{typeof(BaseEnemy<>)} Laser Trigger");
    }
}
