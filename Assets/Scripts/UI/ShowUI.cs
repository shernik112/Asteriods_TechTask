using Pixelplacement;
using Project.System;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class ShowUI : MonoBehaviour
    {
        [SerializeField] private float showTime = default;
        
        private HandlerGameCondition _handlerGameCondition;
        private CanvasGroup _cg;
        private bool _isShowNow;
        
        private void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
            _handlerGameCondition = new HandlerGameCondition();
        }
        
        public void SmoothShow()
        {
            Debug.Log($"{typeof(ShowUI)} SmoothShow");
            _isShowNow = !_isShowNow;
            var startAlpha = _cg.alpha;
            var targetAlpha = _isShowNow ? 1f : 0f;
            _cg.interactable = _isShowNow;
            _cg.blocksRaycasts = _isShowNow;
            _handlerGameCondition.GameCondition = _isShowNow ? GameCondition.Menu : GameCondition.Game;
            Tween.CanvasGroupAlpha(_cg, startAlpha, targetAlpha, showTime, 0f,Tween.EaseInOut);
        }
    }
}
