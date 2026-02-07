using UnityEngine;

public class MoveForward : ManagedBehaviour
{
    [HideInInspector]public float currentSpeed;
    public float defaultSpeed = 1.2f;

    private void Awake()
    {
        currentSpeed = defaultSpeed;
    }

    protected override void OnUpdate()
    {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime, Space.Self);
    }
}
