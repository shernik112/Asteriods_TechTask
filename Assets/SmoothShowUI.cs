using UnityEngine;
using Zenject;
using Pixelplacement;

public class SmoothShowUI : ManagedBehaviour
{
    [SerializeField] private float showTime = default;
    [Inject] private CharacterController _chController;
    private bool _isShowNow;
    private CanvasGroup _cg;

    public override void ManagedInintialize()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable() => _chController.OnHitPlayer += ShowOnHitPlayer;

    private void OnDisable() => _chController.OnHitPlayer -= ShowOnHitPlayer;

    private void ShowOnHitPlayer()
    {
        _isShowNow = true;
        SmoothShow();
    }
    private void SmoothShow()
    {
        Debug.Log($"{typeof(SmoothShowUI)} SmoothShow");
        var startAlpha = _cg.alpha;
        var targetAlpha = _isShowNow ? 1f : 0f;
        _cg.interactable = _isShowNow;
        _cg.blocksRaycasts = _isShowNow;
        Tween.CanvasGroupAlpha(_cg, startAlpha, targetAlpha, showTime, 0f,Tween.EaseInOut);
    }
}
