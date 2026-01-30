using Zenject;

public class PauseHandler : ManagedBehaviour
{
    [Inject] private CharacterController _chController;

    private void OnEnable() => _chController.OnHitPlayer += SetPause;

    private void OnDisable() => _chController.OnHitPlayer -= SetPause;

    private void SetPause() => PauseAll.Add(this);

    protected override bool UpdateWhenPause => true;
    
}
