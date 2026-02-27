using Project.System;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private EventBus _eventBus;
    private Button _button;

    [Inject]
    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RestartGame);
    }
    
    private void OnDestroy() => _button.onClick.RemoveListener(RestartGame);
    private void RestartGame() => _eventBus.OnRestartGame.Invoke();
}
