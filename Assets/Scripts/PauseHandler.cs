using Zenject;

public class PauseHandler : ManagedBehaviour
{
    private CharacterController _chController;
    private RestartInvoke _restartInvoke;

    public void Construct(CharacterController chController, RestartInvoke restartInvoke)
    {
        _chController = chController;
        _restartInvoke = restartInvoke;
    }

    private void OnEnable()
    {
        _chController.OnHitPlayer += SetPause;
    }

    private void OnDisable()
    {
        _chController.OnHitPlayer -= SetPause;
    }

    private void SetPause() => PauseAll.Add(this);

    protected override bool UpdateWhenPause => true;
    
}
