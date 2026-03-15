using System;
using UnityEngine;

namespace Project.Player
{
    public class PlayerModel : Model<PlayerData>
    {
        public event Action<bool> ChangeActive;
        
        public Vector2 Input { get; private set; } = Vector2.zero;

        public PlayerModel(PlayerData data) : base(data) { }
        
        public void SetInput(Vector2 input)
        {
            Input = input.normalized;
        }

        public void Hit()
        {
            IsActive = false;
            ChangeActive?.Invoke(IsActive);
        }

        public void ResetState()
        {
            IsActive = true;
            ChangeActive?.Invoke(IsActive);
            Input = Vector2.zero;
        }
    }
}