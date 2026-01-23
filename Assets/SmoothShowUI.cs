using System;
using UnityEngine;
using Zenject;

public class SmoothShowUI : ManagedBehaviour
{
    [Inject] private CharacterController _chController;

    private void OnEnable() => _chController.OnHitPlayer += SmoothShow;

    private void OnDisable() => _chController.OnHitPlayer += SmoothShow;

    private void SmoothShow()
    {
        
    }
}
