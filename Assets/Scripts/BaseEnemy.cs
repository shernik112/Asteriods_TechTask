using UnityEngine;
using Zenject;

public abstract class BaseEnemy<Tpool> : ManagedBehaviour
    where Tpool : ObjectPool
{
    protected Tpool Pool;
    protected CharacterController ChController;

    [Inject]
    public void Construct(Tpool pool, CharacterController chController)
    {
        Pool = pool;
        ChController = chController;
        ChController.OnHitPlayer += ReturnSelf;
    }
    

    private void OnDisable() => OnDisabled();
    
    protected virtual void OnDisabled() => ChController.OnHitPlayer -= ReturnSelf;
    
    protected abstract void HitBullet();
    protected abstract void HitLaser();

    private void ReturnSelf()
    {
        Pool.Return(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet")) HitBullet();
        if(other.gameObject.CompareTag("Laser")) HitLaser();
    }
}
