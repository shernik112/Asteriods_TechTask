using System;
using UnityEngine;

namespace Project.Player
{
    public class PlayerModel : Model<PlayerData>
    {
        public event Action<bool> ChangeActive;
        
        public Vector2 Input { get; private set; } = Vector2.zero;
        
        private bool _isActive =  true;

        public PlayerModel(PlayerData data) : base(data) { }
        
        public void SetInput(Vector2 input)
        {
            Input = input.normalized;
        }

        public void Hit()
        {
            _isActive = false;
            ChangeActive?.Invoke(_isActive);
        }

        public void ResetState()
        {
            _isActive = true;
            ChangeActive?.Invoke(_isActive);
            Input = Vector2.zero;
        }
    }
}