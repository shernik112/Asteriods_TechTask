using System.Text;
using Project.Player;
using Project.System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class ShowIndicators : ManagedBehaviour
    {
        private StringBuilder _sb = new StringBuilder();
        private PlayerController _playerController;
        private ShootLaser _shootLaser;
        private TMP_Text _text;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
    
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _shootLaser = _playerController.Laser;
        }

        protected override void OnUpdate()
        {
            _sb.Clear();
            _sb.AppendLine(GetPlayerTranform());
            _sb.AppendLine(GetPlayerRotation());
            _sb.AppendLine(GetPlayerVelocity());
            _sb.AppendLine(GetCountLaserShot());
            _sb.AppendLine(GetRechargeTime());
            _text.text = _sb.ToString();
        }

        private string GetPlayerTranform()
        {
            Vector2 pos = _playerController.transform.position;
            return $"Position(X:{pos.x:F2}  Y:{pos.y:F2})";
        }

        private string GetPlayerRotation()
        {
            return $"Rotation({_playerController.transform.rotation.eulerAngles.z.ToString("F2")})";
        }

        private string GetPlayerVelocity()
        {
            Vector2 velocity = _playerController.Rb.linearVelocity;
            return $"Velocity(X:{velocity.x:F2}  Y:{velocity.y:F2})";
        }

        private string GetCountLaserShot() => $"Count Laser Shots({_shootLaser.CurrentCountShоtLaser})";
        private string GetRechargeTime() => $"Recharge Time({Mathf.Max(0,_shootLaser.RechargeDuration - _shootLaser.LastRechargeTime)})";
    }
}
