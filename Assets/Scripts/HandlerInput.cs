using UnityEngine;
using Zenject;

public class HandlerInput : ManagedBehaviour
{
    [Inject] private CharacterController _chController;

    protected override void OnUpdate()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
        _chController.SetInput(input);
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
            _chController.PlayerShoot.TryShoot();
        
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"{typeof(HandlerInput)} Laser Shoot");
            _chController.Laser.TryShoot();
        }
        
    }
}
