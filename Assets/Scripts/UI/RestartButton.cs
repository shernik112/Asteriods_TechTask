using System;
using Project.Core;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton: MonoBehaviour
{
    public event Action OnRestartGame;
    private RestartGame _restartGame;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _restartGame = new RestartGame();
        _button.onClick.AddListener(InvokeRestartGame);
    }
    
    private void OnDestroy() => _button.onClick.RemoveListener(InvokeRestartGame);
    private void InvokeRestartGame() => OnRestartGame.Invoke();
}
