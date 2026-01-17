
using UnityEngine;

public class EnemyTeleport : PlayerTeleport
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            TeleportationObject(other);
    }
}
