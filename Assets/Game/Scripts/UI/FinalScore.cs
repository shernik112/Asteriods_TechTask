using Project.System;
using UnityEngine;
using Zenject;
using TMPro;

namespace Project.UI
{
    public class FinalScore : MonoBehaviour
    {
        private TMP_Text _text;
        private HandlerScore _handlerScore;

        [Inject]
        public void Construct(HandlerScore handlerScore)
        {
            _handlerScore = handlerScore;
        }
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _handlerScore.IsFinalScore += ShowFinalScore;
        }

        private void OnDestroy() =>
            _handlerScore.IsFinalScore -= ShowFinalScore;

        private void ShowFinalScore(int count)
        {
            _text.text = count.ToString();
        }
    }
}