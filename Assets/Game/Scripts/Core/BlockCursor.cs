using UnityEngine;

namespace Project.System
{
    public class BlockCursor
    {
        public void SetCursorCondition(bool isLock)
        {
            if (isLock)
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
