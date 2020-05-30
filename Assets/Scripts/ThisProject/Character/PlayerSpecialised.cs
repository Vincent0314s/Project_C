using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateBehaviour
{
    Idle,
    MovingForward,
    MovingBackward,
    Defending,
    Dodging,
    L,
    LL,
    SL,
    Temp,
    Hurt_Light,
    Hurt_Heavy,
    Hurt_Down,
    Hurt_DefenceBreak,
    Dead,
}


public class PlayerSpecialised : MonoBehaviour
{
    public PlayerStateBehaviour playerState;
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
        SetStateBehaviour(PlayerStateBehaviour.Idle);
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
        cbv.isDefending = Input.GetMouseButtonDown(1);
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
            SetStateBehaviour(PlayerStateBehaviour.Defending);
        if (cbv.isDodging)
            SetStateBehaviour(PlayerStateBehaviour.Dodging);
        if (cbv.IsDead())
            SetStateBehaviour(PlayerStateBehaviour.Dead);
        if (!cbv.IsInCurrentAnimationState(AnimationTag.Combo)) {
            if (direction == Vector3.zero)
                SetStateBehaviour(PlayerStateBehaviour.Idle);
        }
    }



    public void SetStateBehaviour(PlayerStateBehaviour psb) {
        switch (psb) {
            case PlayerStateBehaviour.Idle:

                break;
            case PlayerStateBehaviour.MovingForward:

                break;
            case PlayerStateBehaviour.MovingBackward:

                break;
            case PlayerStateBehaviour.Defending:

                break;
            case PlayerStateBehaviour.Dodging:

                break;
            case PlayerStateBehaviour.L:

                break;
            case PlayerStateBehaviour.LL:

                break;
            case PlayerStateBehaviour.Hurt_Light:

                break;
            case PlayerStateBehaviour.Hurt_Heavy:

                break;
            case PlayerStateBehaviour.Hurt_Down:

                break;
            case PlayerStateBehaviour.Hurt_DefenceBreak:

                break;
            case PlayerStateBehaviour.Dead:

                break;
        }
        playerState = psb;
    }
}
