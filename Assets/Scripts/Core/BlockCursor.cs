using UnityEngine;

namespace Project.System
{
    public class BlockCursor : MonoBehaviour
    {
        public ReferenseSetToggle LockCursor = new ReferenseSetToggle();

        private void Update()
        {
            UpdateLockCondition();
        }

        private void UpdateLockCondition()
        {
            if (LockCursor.True)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
