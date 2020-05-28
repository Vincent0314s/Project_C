using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    private float currentSpeed;

    public float turnSmoothValue = 0.1f;
    private float turnSmoothVelocity;

    public float movementSmoothValue = 0.1f;
    private float movementSmoothVelocity;

    public Transform cam;

    //Private
    Rigidbody rb;
    Animator anim;
    Vector3 direction;

    private bool isRunning;
    [SerializeField]
    private bool isLockingTarget;
  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float animationPercent = (isRunning ? 1 : 0.5f) * direction.magnitude;
        anim.SetFloat("Speed", animationPercent,movementSmoothValue,Time.deltaTime);
        anim.SetBool("isLockingTarget",isLockingTarget);
        if (isLockingTarget) {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.z);
        }
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetRotation = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation,ref turnSmoothVelocity,turnSmoothValue);
            transform.rotation = Quaternion.Euler(0, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0, targetRotation, 0f) * Vector3.forward;
            float targetSpeed = (isRunning ? runSpeed : walkSpeed) * direction.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref movementSmoothVelocity, movementSmoothValue);
            rb.MovePosition(transform.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
        }
   
    }
}
