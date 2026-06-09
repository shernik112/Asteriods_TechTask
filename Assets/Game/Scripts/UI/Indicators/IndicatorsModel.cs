using System;
using Project.Player;
using UnityEngine;

namespace Project.UI
{
    public class IndicatorsModel
    {
        public event Action OnNewText;
        public string Text { get; private set; }
        public Vector2 PlayerPosition => _player.transform.position;
        public Quaternion PlayerRotation => _player.transform.rotation;
        public Rigidbody2D PlayerRigidbody => _player.Mover.Rb;
        public ShootLaser PlayerLaser => _player.Laser;
        
        private readonly PlayerController _player;

        public IndicatorsModel(PlayerController player)
        {
            _player = player;
        }
        
        public float LaserRechargeLeft => 
            Mathf.Max(0, _player.Laser.RechargeDuration - _player.Laser.LastRechargeTime);

        public void SetText(string text)
        {
            Text = text;
            OnNewText?.Invoke();
        }
    }
}