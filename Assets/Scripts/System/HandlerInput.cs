using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class HandlerInput : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;

        private void Update()
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
            _playerController.SetInput(input);
        
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
                _playerController.PlayerShoot.TryShoot();
        
            else if (Input.GetMouseButtonDown(1))
            {
                Debug.Log($"{typeof(HandlerInput)} Laser Shoot");
                _playerController.Laser.TryShoot();
            }
        
        }
    }
}
