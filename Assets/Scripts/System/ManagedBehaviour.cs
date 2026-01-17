using UnityEngine;

public class ManagedBehaviour : ManagedBehaviourBasee
{
    public static ReferenseSetToggle PauseAll = new ReferenseSetToggle();
    public virtual bool UpdateWhenPause { get { return false; } }
    private bool initialized = false;
    public virtual void ManagedUpdate(){}
    public virtual void ManagedLateUpdate(){}
    public virtual void ManagedFixedUpdate(){}
    public virtual void ManagedInintialize(){}

    public sealed override void Update()
    {
        if(CanUpdate())
            ManagedUpdate();
    }

    public sealed override void LateUpdate()
    {
        if(CanUpdate())
            ManagedLateUpdate();
    }

    public sealed override void FixedUpdate()
    {
        if (CanUpdate())
            ManagedFixedUpdate();
    }

    public sealed override void Awake()
    {
        if (!initialized)
        {
            initialized = true;
            ManagedInintialize();
        }
    }

    private bool CanUpdate()
    {
        return UpdateWhenPause || !PauseAll.True;
    }
}
