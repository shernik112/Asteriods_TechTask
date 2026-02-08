using System;
using Project.System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class RestartInvoke: MonoBehaviour
{
    public event Action OnRestartGame;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(InvokeRestartGame);
    }
    
    private void OnDestroy() => _button.onClick.RemoveListener(InvokeRestartGame);
    private void InvokeRestartGame() => OnRestartGame.Invoke();
}
