using System;
using UnityEngine;
using Zenject;

public abstract class BaseEnemy<TPool> : ManagedBehaviour
where TPool : ObjectPool
{ 
    private TPool Pool;
    private CharacterController ChController;
    [Inject]
    public void Construct(TPool pool, CharacterController chController)
    {
        Pool = pool;
        ChController = chController;
        ChController.OnHitPlayer += ReturnSelf;
    }

    private void OnDestroy() => ChController.OnHitPlayer -= ReturnSelf;

    protected abstract void HitBullet();
    protected abstract void HitLaser();

    private void ReturnSelf()
    {
        Debug.Log($"{typeof(BaseEnemy<>)} ReturnSelf ");
        Pool.Return(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet")) HitBullet();
        if(other.gameObject.CompareTag("Laser")) HitLaser();
    }
}
