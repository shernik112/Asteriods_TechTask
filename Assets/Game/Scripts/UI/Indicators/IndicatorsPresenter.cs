using System.Text;
using UnityEngine;

namespace Project.UI
{
    public class IndicatorsPresenter
    {
        private readonly IndicatorsModel _model;
        private readonly IIndicatorsView _view;
        
        private StringBuilder _sb;
        
        public IndicatorsPresenter(
            IndicatorsModel model, 
            IIndicatorsView view)
        {
            _model = model;
            _view = view;
        }

        public void Awake()
        {
            _sb = new StringBuilder();
            _model.OnNewText += NewText;
        }
        
        public void Update()
        {
            _sb.Clear();
            _sb.AppendLine($"Position(X:{_model.PlayerPosition.x:F2}  Y:{_model.PlayerPosition.y:F2})");
            _sb.AppendLine($"Rotation({_model.PlayerRotation.eulerAngles.z:F2})");
            _sb.AppendLine($"Velocity(X:{_model.PlayerLinearVelocity.x:F2}  Y:{_model.PlayerLinearVelocity.y:F2})");
            _sb.AppendLine($"Count Laser Shots({_model.PlayerLaser.CurrentCountShotLaser})");
            _sb.AppendLine($"Recharge Time({_model.LaserRechargeLeft:F2})");

            _model.SetText(_sb.ToString());
        }
        
        public void OnDestroy() =>
            _model.OnNewText -= NewText;

        private void NewText() =>
            _view.ShowText(_model.Text);
    }
}