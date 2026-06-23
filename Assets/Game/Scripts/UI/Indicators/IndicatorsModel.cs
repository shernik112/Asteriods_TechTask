using System;
using Project.Player;
using UnityEngine;

namespace Project.UI
{
    public class IndicatorsModel
    {
        public event Action OnNewText;
        public string Text { get; private set; }
        public Vector2 PlayerPosition => _mover.transform.position;
        public Quaternion PlayerRotation => _mover.transform.rotation;
        public Rigidbody2D PlayerRigidbody => _mover.Rb;
        public ShootLaser PlayerLaser => _laser;

        private readonly PlayerMover _mover;
        private readonly ShootLaser _laser;

        public IndicatorsModel(PlayerMover mover, ShootLaser laser)
        {
            _mover = mover;
            _laser = laser;
        }

        public float LaserRechargeLeft =>
            Mathf.Max(0, _laser.RechargeDuration - _laser.LastRechargeTime);

        public void SetText(string text)
        {
            Text = text;
            OnNewText?.Invoke();
        }
    }
}