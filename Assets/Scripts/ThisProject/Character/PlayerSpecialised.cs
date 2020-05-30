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
    SL,
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
        if (cbv.IsInCurrentAnimationState(AnimationTag.Idle) || cbv.IsInCurrentAnimationState(AnimationTag.Movement))
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0, vertical).normalized;
            cm.SetMovementDirection(direction);
        }
        else {
            cm.StopMoveing();
        }

        cbv.isDodging = Input.GetKeyDown(KeyCode.LeftShift);
        cbv.isDefending = Input.GetMouseButton(1);
        isMouseLeftClick = Input.GetMouseButtonDown(0);
        isForwardKey = Input.GetKeyDown(KeyCode.W);
        isBackwardKey = Input.GetKeyDown(KeyCode.S);
        isTurnRightKey = Input.GetKeyDown(KeyCode.D);
        isTurnLeftKey = Input.GetKeyDown(KeyCode.A);
        

        //if (cm.isMoving) {
        //    if (cm.isForward)
        //    {
        //        SetStateBehaviour(PlayerStateBehaviour.MovingForward);
        //    }
        //    else {
        //        SetStateBehaviour(PlayerStateBehaviour.MovingBackward);
        //    }
        //}

        if (cbv.isDefending)
            SetStateBehaviour(PlayerBasicState.Defending);
        if (cbv.isDodging)
            SetStateBehaviour(PlayerBasicState.Dodging);
        if (cbv.IsDead())
            SetStateBehaviour(PlayerBasicState.Dead);
        if (!cbv.IsInCurrentAnimationState(AnimationTag.Combo)) {
            if (direction == Vector3.zero)
                SetStateBehaviour(PlayerBasicState.Idle);

            SetComboState(PlayerComboState.None);
        }

        cbv.anim.SetBool("isDefending",cbv.isDefending);
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
}
