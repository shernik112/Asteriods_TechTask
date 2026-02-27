using Project.System;
using UnityEngine;
using Zenject;
using TMPro;

namespace Project.UI
{
    public class FinalScore : MonoBehaviour
    {
        private TMP_Text _text;
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _eventBus.IsFinalScore += ShowFinalScore;
        }

        private void OnDestroy() =>
            _eventBus.IsFinalScore -= ShowFinalScore;

        private void ShowFinalScore(int count)
        {
            _text.text = count.ToString();
        }
    }
}