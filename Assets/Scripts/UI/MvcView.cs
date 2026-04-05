using TMPro;
using UnityEngine;

namespace Project.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MvcView : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateCounter(int count)
        {
            _text.text = count.ToString();
        }
    }
}