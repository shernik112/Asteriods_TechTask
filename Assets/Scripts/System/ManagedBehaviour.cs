using UnityEngine;

namespace Project.System
{
    public class ManagedBehaviour : MonoBehaviour
    {
        protected static ReferenseSetToggle PauseAll = new ReferenseSetToggle();
    
        protected virtual bool UpdateWhenPause => false;
        protected virtual void ManagedUpdate(){}
        protected virtual void ManagedFixedUpdate(){}

        private void Update()
        {
            if(CanUpdate())
                ManagedUpdate();
        }

        private void FixedUpdate()
        {
            if (CanUpdate())
                ManagedFixedUpdate();
        }

        private bool CanUpdate()
        {
            return UpdateWhenPause || !PauseAll.True;
        }
    }
}
