using System;
using Project.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.System
{
    public class HandlerInput : ITickable,IDisposable
    {
        private PlayerMover _mover;
        private BulletShoot _bulletShoot;
        private ShootLaser _laser;
        private PauseHandler _pauseHandler;
        private InputActions _inputActions;

        [Inject]
        public void Construct(
            PlayerMover mover,
            BulletShoot bulletShoot,
            ShootLaser laser,
            PauseHandler pauseHandler)
        {
            _mover = mover;
            _bulletShoot = bulletShoot;
            _laser = laser;
            _pauseHandler = pauseHandler;

            _inputActions = new InputActions();
            _inputActions.Enable();

            _inputActions.Player.Laser.performed += OnShootLaser;
        }

        public void Tick()
        {
            if (_pauseHandler.IsPause)
                return;

            var moveDir = _inputActions.Player.Move.ReadValue<Vector2>();
            _mover.SetInput(moveDir);

            if (_inputActions.Player.Shoot.IsPressed())
                _bulletShoot.TryShoot();
        }

        public void Dispose()
        {
            _inputActions.Player.Laser.performed -= OnShootLaser;
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        private void OnShootLaser(InputAction.CallbackContext ctx)
            => ShootLaser();

        private void ShootLaser()
        {
            Debug.Log($"{typeof(HandlerInput)} Laser Shoot");
            _laser.TryShoot();
        }
    }
}
