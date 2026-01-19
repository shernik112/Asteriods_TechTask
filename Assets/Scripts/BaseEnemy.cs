using UnityEngine;

public abstract class BaseEnemy : ManagedBehaviour
{
    protected abstract void HitBullet();
    protected abstract void HitLaser();
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("PlayerBullet")) HitBullet();
        if(other.gameObject.CompareTag("Laser")) HitLaser();
    }
}
