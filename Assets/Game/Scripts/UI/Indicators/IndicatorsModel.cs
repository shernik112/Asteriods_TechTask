using System;

namespace Project.UI
{
    public class IndicatorsModel
    {
        public string Text { get; private set; }

        public event Action OnNewText;

        public void SetText(string text)
        {
            Text = text;
            OnNewText?.Invoke();
        }
    }
}