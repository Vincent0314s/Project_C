using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentInput {

}

public class PlayerInput : MonoBehaviour
{

    private CharacterMovement cm;
    private CharacterBaseValue cbv;

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        cbv.isDodging = Input.GetKeyDown(KeyCode.LeftShift);
        cbv.isDefending = Input.GetMouseButtonDown(1);

        cm.SetMovementDirection(direction);
    }
}
