using Project.System;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        private PlayerData _data;
        private PauseHandler _pauseHandler;
        private Vector2 _input;
        private Rigidbody2D _rb;
        
        public Vector2 GetLinearVelocity() => _rb.linearVelocity;

        [Inject]
        public void Construct(PlayerData data, PauseHandler pauseHandler)
        {
            _data = data;
            _pauseHandler = pauseHandler;
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetInput(Vector2 input) => 
            _input = input.normalized;

        private void FixedUpdate()
        {
            if (_pauseHandler.IsPause)
                return;

            _rb.angularVelocity = Mathf.MoveTowards(
                _rb.angularVelocity,
                -_input.x * _data.RotateSpeed,
                _data.RotateAcceleration * Time.fixedDeltaTime);

            Vector2 targetVelocity = transform.up * _input.y * _data.MoveSpeed;

            _rb.linearVelocity = Vector2.MoveTowards(
                _rb.linearVelocity,
                targetVelocity,
                _data.MoveAcceleration * Time.fixedDeltaTime);
        }

        public void Stop()
        {
            _input = Vector2.zero;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}