using UnityEngine;

public class PlayerTeleport : ManagedBehaviour
{
    [SerializeField] private bool isHorizonWall = default;
    private readonly float _tpOffset = 0.35f;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            TeleportationObject(other);
        
    }

    protected void TeleportationObject(Collider2D otherObj)
    {
        var pos = otherObj.transform.position;
        
        if (isHorizonWall)
            otherObj.transform.position = new Vector2(pos.x, GetTargetPos(pos.y));
        else
            otherObj.transform.position = new Vector2(GetTargetPos(pos.x), pos.y);
    }

    private float GetTargetPos(float tpPos)
    {
        return -Mathf.Sign(tpPos) * (Mathf.Abs(tpPos) - _tpOffset);
    }
}
