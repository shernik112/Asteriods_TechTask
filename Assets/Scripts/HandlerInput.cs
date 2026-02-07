using UnityEngine;
using Zenject;

public class HandlerInput : ManagedBehaviour
{
    [Inject] private CharacterController _chController;

    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"{typeof(HandlerInput)} Laser Shoot");
            _chController.Laser.TryShoot();
        }
    }
}
