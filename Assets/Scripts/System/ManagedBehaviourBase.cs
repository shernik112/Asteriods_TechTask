using UnityEngine;

public class ManagedBehaviourBase : MonoBehaviour
{
    protected virtual void Update(){}
    protected virtual void LateUpdate() {}
    protected virtual void  FixedUpdate(){}
    public virtual void Awake(){}
}
