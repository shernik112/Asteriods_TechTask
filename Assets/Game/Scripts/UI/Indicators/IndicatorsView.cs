using TMPro;
using UnityEngine;

namespace Project.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class IndicatorsView : MonoBehaviour, IIndicatorsView
    {
        private TMP_Text _text;

        private void Awake() =>
            _text = GetComponent<TMP_Text>();

        public void ShowText(string value) =>
            _text.text = value;
    }
}