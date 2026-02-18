using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class HandlerInput : MonoBehaviour
    {
        private PlayerController _playerController;
        private PauseHandler _pauseHandler;

        [Inject]
        public void Construct(
            PlayerController playerController,
            PauseHandler pauseHandler)
        {
            _playerController = playerController;
            _pauseHandler = pauseHandler;
        }
        
        private void Update()
        {
            if (_pauseHandler.IsPause) 
                return;
            
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Clamp01(Input.GetAxisRaw("Vertical")));
            _playerController.SetInput(input);
        
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log($"{typeof(HandlerInput)} Laser Shoot");
                _playerController.Laser.TryShoot();
            }
            else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
                _playerController.BulletShoot.TryShoot();
        }
    }
}
