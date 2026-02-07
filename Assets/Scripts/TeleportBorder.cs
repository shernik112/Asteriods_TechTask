using UnityEngine;

public class TeleportBorder : ManagedBehaviour
{
    [SerializeField] private bool isHorizonWall = default;
    
    private readonly float _teleportOffset = 0.35f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IEnemy>(out var enemy))
        {
            if (enemy.IsFirstEnterToTeleport)
            {
                enemy.IsFirstEnterToTeleport = false;
                return;
            }
            
            TeleportationObject(other);
        }
        if (other.TryGetComponent<CharacterController>(out var player))
            TeleportationObject(other);
    }

    private void TeleportationObject(Collider2D otherObj)
    {
        var pos = otherObj.transform.position;
        
        if (isHorizonWall)
            otherObj.transform.position = new Vector2(pos.x, GetTargetPos(pos.y));
        else
            otherObj.transform.position = new Vector2(GetTargetPos(pos.x), pos.y);
    }

    private float GetTargetPos(float tpPos)
    {
        return -Mathf.Sign(tpPos) * (Mathf.Abs(tpPos) - _teleportOffset);
    }
}
