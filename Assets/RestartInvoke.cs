using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class RestartInvoke: ManagedBehaviour
{
    public event Action OnRestartGame;
    private Button _button;
    public override void ManagedInintialize()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable() => _button.onClick.AddListener(InvokeRestartGame);

    private void OnDisable() => _button.onClick.RemoveListener(InvokeRestartGame);
    private void InvokeRestartGame() => OnRestartGame.Invoke();
}
