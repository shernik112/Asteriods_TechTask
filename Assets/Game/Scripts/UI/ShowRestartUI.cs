using Pixelplacement;
using Project.System;
using UnityEngine;

namespace Project.UI
{
    public class ShowRestartUI : MonoBehaviour
    {
        [field:SerializeField] public int ShowTimeMs { get; private set; }
        
        private CanvasGroup _cg;
        private bool _isShowNow;
        
        private void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
        }
        
        public void SmoothShow()
        {
            Debug.Log($"{typeof(ShowRestartUI)} SmoothShow");
            _isShowNow = !_isShowNow;
            var startAlpha = _cg.alpha;
            var targetAlpha = _isShowNow ? 1f : 0f;
            _cg.interactable = _isShowNow;
            _cg.blocksRaycasts = _isShowNow;
            Tween.CanvasGroupAlpha(_cg, startAlpha, targetAlpha, ShowTimeMs, 0f,Tween.EaseInOut);
        }
    }
}
