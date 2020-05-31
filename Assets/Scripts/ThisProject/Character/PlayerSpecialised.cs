using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerBasicState
{
    Idle,
    MovingForward,
    MovingBackward,
    Defending,
    Dodging,
    Hurt_Light,
    Hurt_Heavy,
    Hurt_Down,
    Hurt_DefenceBreak,
    Dead,
}

public enum PlayerComboState {
    None,
    L,
    LL,
    WL,
    WLSL,
    WWL,
    CanNextCombo,
}


public class PlayerSpecialised : MonoBehaviour
{
    public PlayerBasicState playerState;
    public PlayerComboState playerCombo;
    private CharacterMovement cm;
    private CharacterBaseValue cbv;

    private Vector3 direction;

    public bool isMouseLeftClick { get; private set; }
    public bool isForwardKey { get; private set; }
    public bool isBackwardKey { get; private set; }
    public bool isTurnRightKey { get; private set; }
    public bool isTurnLeftKey { get; private set; }


    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        SetStateBehaviour(PlayerBasicState.Idle);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;
        cm.SetMovementDirection(direction);


        cbv.isDodging = Input.GetKeyDown(KeyCode.LeftShift);
        cbv.isDefending = Input.GetMouseButton(1);
        isMouseLeftClick = Input.GetMouseButtonDown(0);
        isForwardKey = Input.GetKeyDown(KeyCode.W);
        isBackwardKey = Input.GetKeyDown(KeyCode.S);
        isTurnRightKey = Input.GetKeyDown(KeyCode.D);
        isTurnLeftKey = Input.GetKeyDown(KeyCode.A);

        
        if (cbv.IsDead())
            SetStateBehaviour(PlayerBasicState.Dead);

        if (!Input.anyKey && cbv.IsInOriginalAnimationState())
        {
            SetStateBehaviour(PlayerBasicState.Idle);
        }
        else {
            if (cm.isMoving)
            {
                if (cm.isForward)
                {
                    SetStateBehaviour(PlayerBasicState.MovingForward);
                }
                else
                {
                    SetStateBehaviour(PlayerBasicState.MovingBackward);
                }
            }

            if (cbv.isDefending)
                SetStateBehaviour(PlayerBasicState.Defending);
            if (cbv.isDodging)
                SetStateBehaviour(PlayerBasicState.Dodging);
        }

        cbv.anim.SetBool("isDefending",cbv.isDefending);
        cbv.anim.SetBool("isDodging",cbv.isDodging);
        cbv.anim.SetFloat("Horizontal",direction.x);
        cbv.anim.SetFloat("Vertical",direction.z);
    }



    public void SetStateBehaviour(PlayerBasicState psb) {
        switch (psb) {
            case PlayerBasicState.Idle:

                break;
            case PlayerBasicState.MovingForward:

                break;
            case PlayerBasicState.MovingBackward:

                break;
            case PlayerBasicState.Defending:

                break;
            case PlayerBasicState.Dodging:
                break;
            case PlayerBasicState.Hurt_Light:

                break;
            case PlayerBasicState.Hurt_Heavy:

                break;
            case PlayerBasicState.Hurt_Down:

                break;
            case PlayerBasicState.Hurt_DefenceBreak:

                break;
            case PlayerBasicState.Dead:

                break;
        }
        playerState = psb;
    }

    public void SetComboState(PlayerComboState pcs) {
        playerCombo = pcs;
    }

    public bool IsPlayerOnlyMoveing() {
        return playerState == PlayerBasicState.Idle || playerState == PlayerBasicState.MovingBackward || playerState == PlayerBasicState.MovingForward;
    }
}
