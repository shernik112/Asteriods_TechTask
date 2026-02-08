using UnityEngine;

namespace Project.System
{
    public class ManagedBehaviour : MonoBehaviour
    {
        protected virtual void ManagedUpdate(){}
        protected virtual void ManagedFixedUpdate(){}

        private void Update()
        {
            ManagedUpdate();
        }

        private void FixedUpdate()
        {
            ManagedFixedUpdate();
        }
    }
}
