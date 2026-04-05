using System;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    public event Action OnRestartGame;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RestartGame);
    }
    
    private void OnDestroy() => _button.onClick.RemoveListener(RestartGame);
    private void RestartGame() => OnRestartGame?.Invoke();
}
