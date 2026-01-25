using UnityEngine;

public class MoveForward : ManagedBehaviour
{
    public float defaultSpeed = 1.2f;
    [HideInInspector]public float _currentSpeed;
    public override void ManagedInintialize()
    {
        _currentSpeed = defaultSpeed;
    }

    protected override void ManagedUpdate()
    {
        transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime, Space.Self);
    }
}
