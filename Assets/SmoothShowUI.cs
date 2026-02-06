using UnityEngine;
using Zenject;
using Pixelplacement;

public class SmoothShowUI : ManagedBehaviour
{
    [SerializeField] private float showTime = default;
    private HandlerGameCondition _gameCondition;
    private CharacterController _chController;
    private RestartInvoke _restartInvoke;
    
    [Inject]
    public void Construct(
        HandlerGameCondition gameCondition, 
        CharacterController chController,
        RestartInvoke restartInvoke)
    {
        _gameCondition = gameCondition;
        _chController = chController;
        _restartInvoke = restartInvoke;
    }
    private bool _isShowNow;
    private CanvasGroup _cg;

    private void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _chController.OnHitPlayer += SmoothShow;
        _restartInvoke.OnRestartGame += SmoothShow;
    }
    private void OnDisable()
    {
        _chController.OnHitPlayer -= SmoothShow;
        _restartInvoke.OnRestartGame -= SmoothShow;
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
