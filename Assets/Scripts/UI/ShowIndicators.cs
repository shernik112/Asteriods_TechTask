using Project.Player;
using System.Text;
using UnityEngine;
using Zenject;
using TMPro;

namespace Project.UI
{
    public class ShowIndicators : MonoBehaviour
    {
        private StringBuilder _sb = new StringBuilder();
        private PlayerController _playerController;
        private TMP_Text _text;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
    
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _sb.Clear();
            _sb.AppendLine(GetPlayerTransform());
            _sb.AppendLine(GetPlayerRotation());
            _sb.AppendLine(GetPlayerVelocity());
            _sb.AppendLine(GetCountLaserShot());
            _sb.AppendLine(GetRechargeTime());
            _text.text = _sb.ToString();
        }

        private string GetPlayerTransform()
        {
            Vector2 pos = _playerController.View.transform.position;
            return $"Position(X:{pos.x:F2}  Y:{pos.y:F2})";
        }

        private string GetPlayerRotation()
        {
            return $"Rotation({_playerController.View.transform.rotation.eulerAngles.z.ToString("F2")})";
        }

        private string GetPlayerVelocity()
        {
            Vector2 velocity = _playerController.Rb.linearVelocity;
            return $"Velocity(X:{velocity.x:F2}  Y:{velocity.y:F2})";
        }

        private string GetCountLaserShot() => $"Count Laser Shots({_playerController.Laser.CurrentCountShоtLaser})";
        private string GetRechargeTime() => $"Recharge Time({Mathf.Max(0,_playerController.Laser.RechargeDuration - _playerController.Laser.LastRechargeTime):F2})";
    }
}
