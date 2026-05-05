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

        public Rigidbody2D Rb { get; private set; }

        [Inject]
        public void Construct(PlayerData data, PauseHandler pauseHandler)
        {
            _data = data;
            _pauseHandler = pauseHandler;
        }
        
        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }

        public void SetInput(Vector2 input) => 
            _input = input.normalized;

        private void FixedUpdate()
        {
            if (_pauseHandler.IsPause)
                return;

            Rb.angularVelocity = Mathf.MoveTowards(
                Rb.angularVelocity,
                -_input.x * _data.RotateSpeed,
                _data.RotateAcceleration * Time.fixedDeltaTime);

            Vector2 targetVelocity = transform.up * _input.y * _data.MoveSpeed;

            Rb.linearVelocity = Vector2.MoveTowards(
                Rb.linearVelocity,
                targetVelocity,
                _data.MoveAcceleration * Time.fixedDeltaTime);
        }

        public void Stop()
        {
            _input = Vector2.zero;
            Rb.linearVelocity = Vector2.zero;
            Rb.angularVelocity = 0f;
        }
    }
}