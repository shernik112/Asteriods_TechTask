using Project.Enemies;
using Project.System;
using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerView : MonoBehaviour, ITeleportedReaction
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sprite;
        private PlayerModel _model;
        private EventBus _eventBus;
        private MainAudio _mainAudio;
        private PauseHandler _pauseHandler;

        public void Init(PlayerModel model, 
            EventBus eventBus, 
            MainAudio mainAudio, 
            PauseHandler pauseHandler)
        {
            _rb = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();

            _model = model;
            _eventBus = eventBus;
            _mainAudio = mainAudio;
            _pauseHandler = pauseHandler;
            
            _sprite.sprite = _model.Data.sprite;
            _model.ChangeActive += ChangeActive;
            ChangeActive(true);
        }
        
        private void OnDestroy()
        {
            if (_model != null) _model.ChangeActive -= ChangeActive;
        }

        private void FixedUpdate()
        {
            if (_pauseHandler.IsPause) 
                return;
            
            var input = _model.Input;
            
            _rb.angularVelocity = Mathf.MoveTowards(_rb.angularVelocity, -input.x * _model.Data.rotateSpeed,
                _model.Data.rotateAcceleration * Time.fixedDeltaTime);
            
            Vector2 targetVelocity = transform.up * input.y * _model.Data.moveSpeed;
            _rb.linearVelocity = Vector2.MoveTowards(_rb.linearVelocity, targetVelocity, _model.Data.moveAcceleration * Time.fixedDeltaTime);
        }

        public void TeleportReaction() =>
            _mainAudio?.PlaySfx(_model.Data.dashClip);
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IEnemy>(out var enemy))
            {
                _mainAudio?.PlaySfx(_model.Data.destructionClip);
                _eventBus.OnHitPlayer?.Invoke();
                _model.Hit();
                SetDefaultValues();
            }

            Debug.Log($"{typeof(PlayerView)} OnCollisionEnter");
        }
        
        private void ChangeActive(bool active) =>
            _sprite.enabled = active;

        private void SetDefaultValues()
        {
            _rb.angularVelocity = 0f;
            _rb.linearVelocity = Vector2.zero;
        }

        public void ResetState()
        {
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}