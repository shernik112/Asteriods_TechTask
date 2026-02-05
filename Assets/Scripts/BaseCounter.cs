using TMPro;
using Zenject;

public abstract class BaseCounter : ManagedBehaviour
{
    [Inject] protected CharacterController ChController;
    protected int Count;
    protected TMP_Text Text;

    public override void ManagedInintialize()
    {
        Text = GetComponent<TMP_Text>();
    }

    protected virtual void OnEnable() => ChController.OnHitPlayer += ResetCount;
    protected virtual void OnDisable() => ChController.OnHitPlayer -= ResetCount;

    protected virtual void ResetCount()
    {
        Count = default;
        Text.text = default;
    }

    protected virtual void AddToCount(int count){}
}
