using Pixelplacement;
using Project.System;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class SmoothShowUI : MonoBehaviour
    {
        [SerializeField] private float showTime = default;
        
        private HandlerGameCondition _gameCondition;
        private PlayerController _playerController;
        private RestartButton _restartButton;
        private CanvasGroup _cg;
        private bool _isShowNow;
    
        [Inject]
        public void Construct(
            HandlerGameCondition gameCondition, 
            PlayerController playerController,
            RestartButton restartButton)
        {
            _gameCondition = gameCondition;
            _playerController = playerController;
            _restartButton = restartButton;
        }

        private void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
            _playerController.OnHitPlayer += SmoothShow;
            _restartButton.OnRestartGame += SmoothShow;
        }
    
        private void OnDestroy()
        {
            _playerController.OnHitPlayer -= SmoothShow;
            _restartButton.OnRestartGame -= SmoothShow;
        }
    
        private void SmoothShow()
        {
            Debug.Log($"{typeof(SmoothShowUI)} SmoothShow");
            _isShowNow = !_isShowNow;
            var startAlpha = _cg.alpha;
            var targetAlpha = _isShowNow ? 1f : 0f;
            _cg.interactable = _isShowNow;
            _cg.blocksRaycasts = _isShowNow;
            _gameCondition.GameCondition = _isShowNow ? GameCondition.Menu : GameCondition.Game;
            Tween.CanvasGroupAlpha(_cg, startAlpha, targetAlpha, showTime, 0f,Tween.EaseInOut);
        }
    }
}
