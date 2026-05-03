using System.Text;
using Project.Player;
using UnityEngine;

namespace Project.UI
{
    public class IndicatorsPresenter
    {
        private readonly PlayerController _player;
        private readonly IndicatorsModel _model;
        private readonly IIndicatorsView _view;
        
        public IndicatorsPresenter(
            PlayerController player, 
            IndicatorsModel model, 
            IIndicatorsView view)
        {
            _player = player;
            _model = model;
            _view = view;
        }

        public void Awake() =>
            _model.OnNewText += NewText;
        
        public void Update()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Position(X:{_player.transform.position.x:F2}  Y:{_player.transform.position.y:F2})");
            sb.AppendLine($"Rotation({_player.transform.rotation.eulerAngles.z:F2})");
            sb.AppendLine($"Velocity(X:{_player.Rb.linearVelocity.x:F2}  Y:{_player.Rb.linearVelocity.y:F2})");
            sb.AppendLine($"Count Laser Shots({_player.Laser.CurrentCountShоtLaser})");
            sb.AppendLine($"Recharge Time({Mathf.Max(0, _player.Laser.RechargeDuration - _player.Laser.LastRechargeTime):F2})");

            _model.SetText(sb.ToString());
        }
        
        public void OnDestroy() =>
            _model.OnNewText -= NewText;

        private void NewText() =>
            _view.ShowText(_model.Text);
    }
}