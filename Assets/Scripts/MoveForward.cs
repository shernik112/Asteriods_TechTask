using UnityEngine;

public class MoveForward : ManagedBehaviour
{
    public float defaultSpeed = 1.2f;
    [HideInInspector]public float currentSpeed;

    private void Awake()
    {
        currentSpeed = defaultSpeed;
    }

    protected override void OnUpdate()
    {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime, Space.Self);
    }
}
