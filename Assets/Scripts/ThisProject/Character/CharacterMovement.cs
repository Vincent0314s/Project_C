using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool isPlayer;
    public float walkSpeed = -3f;
    public float runSpeed = 5f;
    private float currentSpeed;

    public float turnSmoothValue = 0.15f;
    private float turnSmoothVelocity;

    public float forwardMovementSmoothValue = 0.35f;
    private float forwardMovementSmoothVelocity;

    public float backwardMovementSmoothValue = 0.1f;
    private float backwardMovementSmoothVelocity;

    public bool isMoving { get; private set; }
    public bool isForward { get; private set; }

    private Vector3 direction;
    private CharacterBaseValue cbv;

    private void Start()
    {
        cbv = GetComponent<CharacterBaseValue>();
    }

    public void SetMovementDirection(Vector3 _direction) {
        direction = _direction;
    }

    void Update() {
        if (isMoving)
        {
            if (isForward)
            {
                cbv.anim.SetFloat("ForwardSpeed", direction.magnitude, forwardMovementSmoothValue, Time.deltaTime);
                cbv.anim.SetFloat("BackwardSpeed", 0);
            }
            else
            {
                cbv.anim.SetFloat("BackwardSpeed", direction.magnitude, backwardMovementSmoothValue, Time.deltaTime);
                cbv.anim.SetFloat("ForwardSpeed", 0);
            }
        }
        else {
            cbv.anim.SetFloat("ForwardSpeed", 0f, forwardMovementSmoothValue, Time.deltaTime);
            cbv.anim.SetFloat("BackwardSpeed", 0f, forwardMovementSmoothValue, Time.deltaTime);
        }
        cbv.anim.SetBool("isMoving",isMoving);
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            if (direction.magnitude >= 0.1f)
            {
                isMoving = true;
                float targetRotation = Mathf.Atan2(direction.x, Mathf.Abs(direction.z)) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothValue);
                transform.rotation = Quaternion.Euler(0, angle, 0f);
                if (direction.z >= 0)
                {
                    isForward = true;
                    Vector3 moveDir = Quaternion.Euler(0, targetRotation, 0f) * Vector3.forward;
                    float targetSpeed = runSpeed * direction.magnitude;
                    currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref forwardMovementSmoothVelocity, forwardMovementSmoothValue);
                    cbv.rb.MovePosition(transform.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    isForward = false;
                    Vector3 moveDir = Quaternion.Euler(0, targetRotation, 0f) * Vector3.forward;
                    float targetSpeed = walkSpeed * direction.magnitude;
                    currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref backwardMovementSmoothVelocity, backwardMovementSmoothValue);
                    cbv.rb.MovePosition(transform.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
                }
            }
            else {
                isMoving = false;
            }
        }
        else {
            //Enemy
        }
      
    }

    public void StopMoveing() {
        currentSpeed = 0;
    }
}
