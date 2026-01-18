using UnityEngine;

public class MoveForward : ManagedBehaviour
{
    private readonly float _speed = 1.5f;
    protected override void ManagedUpdate()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.Self);
    }
}
