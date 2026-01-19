using UnityEngine;

public class MoveForward : ManagedBehaviour
{
    public float speed = 1.5f;
    protected override void ManagedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
}
