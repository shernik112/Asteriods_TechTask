
public class ManagedBehaviour : ManagedBehaviourBase
{
    public static ReferenseSetToggle PauseAll = new ReferenseSetToggle();
    protected virtual bool UpdateWhenPause => false;
    private bool _initialized = false;
    protected virtual void ManagedUpdate(){}
    protected virtual void ManagedLateUpdate(){}
    protected virtual void ManagedFixedUpdate(){}
    public virtual void ManagedInintialize(){}

    protected sealed override void Update()
    {
        if(CanUpdate())
            ManagedUpdate();
    }

    protected sealed override void LateUpdate()
    {
        if(CanUpdate())
            ManagedLateUpdate();
    }

    protected sealed override void FixedUpdate()
    {
        if (CanUpdate())
            ManagedFixedUpdate();
    }

    public sealed override void Awake()
    {
        if (!_initialized)
        {
            _initialized = true;
            ManagedInintialize();
        }
    }

    private bool CanUpdate()
    {
        return UpdateWhenPause || !PauseAll.True;
    }
}
