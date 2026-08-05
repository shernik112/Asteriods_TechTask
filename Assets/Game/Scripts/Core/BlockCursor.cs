using UnityEngine;

namespace Project.System
{
    public class BlockCursor
    {
        public void SetLocked(bool isLocked)
        {
            Cursor.lockState = isLocked 
                ? CursorLockMode.Locked 
                : CursorLockMode.None;
            Cursor.visible = !isLocked;
        }
    }
}
