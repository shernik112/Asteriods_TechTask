using UnityEngine;

public class BlockCursor : ManagedBehaviour
{
    public ReferenseSetToggle LockCursor = new ReferenseSetToggle();
    protected override bool UpdateWhenPause => true;

    protected override void ManagedUpdate()
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
