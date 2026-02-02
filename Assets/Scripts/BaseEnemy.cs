using System;
using UnityEngine;
using Zenject;

public abstract class BaseEnemy<TPool> : ManagedBehaviour
where TPool : ObjectPool
{ 
    protected TPool Pool;
    protected CharacterController _chController;
    private HandlerScore _handlerScore;
    [Inject]
    public void Construct(TPool pool, CharacterController chController, HandlerScore handlerScore)
    {
        Pool = pool;
        _chController = chController;
        _handlerScore = handlerScore;
    }

    private void OnEnable()
    {
        if (_chController != null) _chController.OnHitPlayer += ReturnSelf;
    }

    private void OnDisable()
    {
        if (_chController != null) _chController.OnHitPlayer -= ReturnSelf;
    }

    protected abstract void HitBullet();
    protected abstract void HitLaser();

    private void ReturnSelf()
    {
        Debug.Log($"{typeof(BaseEnemy<>)} ReturnSelf ");
        Pool.Return(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") || other.gameObject.CompareTag("Laser"))
        {
            if(this is AsteroidBehaviour) _handlerScore.CountDefeatedEnemy(typeof(AsteroidBehaviour));
            if(this is UFOBehaviour) _handlerScore.CountDefeatedEnemy(typeof(UFOBehaviour));
        }
        if(other.gameObject.CompareTag("PlayerBullet")) HitBullet();
        else if(other.gameObject.CompareTag("Laser")) HitLaser();
    }
}
