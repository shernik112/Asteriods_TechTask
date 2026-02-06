using UnityEngine;

public class ManagedBehaviour : MonoBehaviour
{
    public static ReferenseSetToggle PauseAll = new ReferenseSetToggle();
    protected virtual bool UpdateWhenPause => false;
    protected virtual void OnUpdate(){}
    protected virtual void OnLateUpdate(){}
    protected virtual void OnFixedUpdate(){}

    private void Update()
    {
        if(CanUpdate())
            OnUpdate();
    }

    private void LateUpdate()
    {
        if(CanUpdate())
            OnLateUpdate();
    }

    private void FixedUpdate()
    {
        if (CanUpdate())
            OnFixedUpdate();
    }

    private bool CanUpdate()
    {
        return UpdateWhenPause || !PauseAll.True;
    }
}
