using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        LockCursor();
    }

    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnLockCursor() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
