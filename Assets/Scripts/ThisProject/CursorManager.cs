using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        LockCursor();
    }

    public static void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnLockCursor() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
