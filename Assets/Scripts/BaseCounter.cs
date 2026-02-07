using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public abstract class BaseCounter : ManagedBehaviour
{
    protected int Count;
    protected TMP_Text Text;
    private CharacterController _сharacterController;

    [Inject]
    public void Construct(CharacterController characterController)
    {
        _сharacterController = characterController;
    }
    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
        _сharacterController.OnHitPlayer += ResetCount;
    }
    protected virtual void OnDestroy() => _сharacterController.OnHitPlayer -= ResetCount;

    protected virtual void ResetCount()
    {
        Count = default;
        Text.text = default;
    }

    protected virtual void AddToCount(int count){}
}
