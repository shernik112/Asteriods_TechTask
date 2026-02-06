using UnityEngine;
using Zenject;

public abstract class BaseEnemy<TPool> : ManagedBehaviour
where TPool : ObjectPool
{ 
    protected TPool Pool;
    protected CharacterController ChController { get; private set; }
    private HandlerScore _handlerScore;
    
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

    protected abstract void HitBullet();
    protected abstract void HitLaser();

    private void ReturnSelf()
    {
        Debug.Log($"{typeof(BaseEnemy<>)} ReturnSelf ");
        Pool.ReturnToPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Hit object: {other.gameObject.name}, tag={other.gameObject.tag}, root={other.transform.root.name}");
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if(this is AsteroidBehaviour) _handlerScore.CountDefeatedEnemy(typeof(AsteroidBehaviour));
            if(this is UfoBehaviour) _handlerScore.CountDefeatedEnemy(typeof(UfoBehaviour));
        }
        if(other.gameObject.CompareTag("PlayerBullet")) HitBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{typeof(BaseEnemy<>)} Laser Trigger");
        if (other.gameObject.CompareTag("Laser"))
        {
            if(this is AsteroidBehaviour) _handlerScore.CountDefeatedEnemy(typeof(AsteroidBehaviour));
            if(this is UfoBehaviour) _handlerScore.CountDefeatedEnemy(typeof(UfoBehaviour));
            HitLaser();
        }
    }
}
