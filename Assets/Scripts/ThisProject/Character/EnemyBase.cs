using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIState;

public enum Direction {
    Stop,
    Forward,
    Backward,
    Left,
    Right,
}

public enum EnemyActions
{
    Idle,
    Move_Forward,
    Move_Backward,
    Move_Left,
    Move_Right,
    Defending,
    L,
    LL,
    WL,
    WLSL,
    Temp,
    Hurt_Light,
    Hurt_Heavy,
    Hurt_DefenceBreak,
    Hurt_Down,
    Dodge_Left,
    Dodge_Right,
    Dodge_Forward,
}

[System.Serializable]
public class EnemyPossibleBehaviour {
    public EnemyActions behaviour;
    public int percentage;
}

public enum EnemyNodes
{
    Idle,
    NotCloseTarget,
    CloseTarget,
    Defending,
    Dodging,
    L,
    LL,
    WL,
    WLSL,
    Temp,
    Hurt,
    Dead,
}

[System.Serializable]
public class EnemyDoStateBehaviour {

    public EnemyNodes state;
    [ArrayElementTitle("behaviour")]
    public List<EnemyPossibleBehaviour> behaviours = new List<EnemyPossibleBehaviour>();
    public int totalPercentage;

    public void Init() {
        foreach (var per in behaviours)
        {
            totalPercentage += per.percentage;
        }
    }
}

public class EnemyBase : MonoBehaviour
{
    public Vector2 reactionTime = new Vector2(1,2.5f);
    public Vector2 defendingTime = new Vector2(2,5f);
    public Vector2 movementTime = new Vector2(3,5);
    [Range(1f,10f)]
    public float stoppedDistance = 3f;
    private float currentReactionTime = 0;
    private float currentDefendingTime = 0;
    public EnemyActions actions;
    public EnemyNodes nodes;

    //Node and Action
    public List<EnemyDoStateBehaviour> behaviours = new List<EnemyDoStateBehaviour>();
    protected EnemyDoStateBehaviour currentBehaviour;
    private int randomPercentage = 0;

    private Vector3 direction;

    protected CharacterMovement cm;
    protected CharacterBaseValue cbv;
    protected EnemyStateMachine<EnemyBase> stateMachine;

    public virtual void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        stateMachine = new EnemyStateMachine<EnemyBase>(this);

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].Init();
        }
    }

    public virtual void Update() {
        AnimController();
    }

    #region Common Method
    public float GetRandomReactionTime() {
        return UnityEngine.Random.Range(reactionTime.x,reactionTime.y);
    }

    public float GetRandomMovementTime() {
        return UnityEngine.Random.Range(movementTime.x,movementTime.y);
    }

    public float GetRandomDefendingTime() {
        return UnityEngine.Random.Range(defendingTime.x,defendingTime.y);

    }

    public virtual void IdleEnter() {
        PlayAnim(EnemyActions.Idle);
        StartCoroutine(ChangeToNextState(GetRandomReactionTime(),()=> { IdleNode(); }));
    }

    public bool IsCloseTarget()
    {
        return Fundamental.IsCloseTarget(this.transform, cbv.target, stoppedDistance);
    }

    //Movement
    public void LookTarget()
    {
        cbv.CameraLookAtTarget();
        cm.RotateToTarget();
    }

    void Move(Direction _direction)
    {
        LookTarget();
        switch (_direction)
        {
            case Direction.Forward:
                direction = Vector3.forward;
                cm.SetMovementDirection(direction);
                break;
            case Direction.Backward:
                direction = Vector3.back;
                cm.SetMovementDirection(direction);
                break;
            case Direction.Left:
                direction = Vector3.left;
                cm.SetMovementDirection(direction);
                break;
            case Direction.Right:
                direction = Vector3.right;
                cm.SetMovementDirection(direction);
                break;
            case Direction.Stop:
                direction = Vector3.zero;
                cm.SetMovementDirection(direction);
                break;
        }
    }

    void Dodge(Direction _direction) {
        switch (_direction)
        {
            case Direction.Forward:
                direction = Vector3.forward;
                break;
            case Direction.Left:
                direction = Vector3.left;
                break;
            case Direction.Right:
                direction = Vector3.right;
                break;
        }
    }

    void PlayAnim(EnemyActions _action) {
        cbv.anim.Play(_action.ToString(),0);
    }

    #endregion

    public void IdleNode()
    {
        if (IsCloseTarget())
        {
            nodes = EnemyNodes.CloseTarget;
            SetActionBehaviour(GetPercentageAction(nodes));
        }
        else
        {
            nodes = EnemyNodes.NotCloseTarget;
            SetActionBehaviour(GetPercentageAction(nodes));
        }
    }

    public virtual void MovementEnter() {
        StartCoroutine(ChangeToNextState(GetRandomMovementTime(), () => { BackToIdle_Immediately();}));
    }

    public void BackToIdle_Immediately() {
        nodes = EnemyNodes.Idle;
        SetActionBehaviour(GetPercentageAction(nodes));
    }

    public void BackToIdle_AnimationCompeleted()
    {
        if (cbv.IsInCurrentAnimationState(AnimationTag.Combo) || cbv.IsInCurrentAnimationState(AnimationTag.Dodging))
        {
            if (cbv.GetCurrentAnimationStateNormalizedTime() >= 1f)
            {
                BackToIdle_Immediately();
            }
        }
    }

    public void NextToTarget() {
        nodes = EnemyNodes.CloseTarget;
        SetActionBehaviour(GetPercentageAction(nodes));
    }

    IEnumerator ChangeToNextState(float timer, Action trigger)
    {
        yield return new WaitForSeconds(timer);
        //NextState
        trigger?.Invoke();
    }

    public void Move_Forward() {
        Move(Direction.Forward);
    }

    public void Move_Backward() {
        Move(Direction.Backward);
    }

    public void Move_Left() {
        Move(Direction.Left);
    }

    public void Move_Right() {
        Move(Direction.Right);
    }

    public void StopMoving() {
        cm.StopMoveing();
        Move(Direction.Stop);
    }

    public virtual void DefendingEnter() {
        cbv.isDefending = true;
        StartCoroutine(ChangeToNextState(GetRandomDefendingTime(),()=> { BackToIdle_Immediately(); }));
    }

    public void DefendingExit() {
        cbv.isDefending = false;
    }

    public void DodgingEnter() {
        cbv.isDodging = true;
        //Invincible Control

    }

    public void Dodge_Forward() {
        Dodge(Direction.Forward);
    }

    public void Dodge_Right()
    {
        Dodge(Direction.Right);
    }

    public void Dodge_Left()
    {
        Dodge(Direction.Left);
    }

    public void DodgingExit() {
        cbv.isDodging = false;
        StopMoving();
    }

    public void L_Enter() {
        PlayAnim(EnemyActions.L);
    }

    #region Detect Node and Action
    public EnemyActions GetPercentageAction(EnemyNodes _nodes) {
        for (int i = 0; i < behaviours.Count; i++)
        {
            if (behaviours[i].state == _nodes)
            {
                currentBehaviour = behaviours[i];
                randomPercentage = UnityEngine.Random.Range(0, currentBehaviour.totalPercentage);
                break;
            }
        }
        for (int i = 0; i < currentBehaviour.behaviours.Count; i++)
        {
            if (randomPercentage <= currentBehaviour.behaviours[i].percentage)
            {
                return currentBehaviour.behaviours[i].behaviour;
            }
            else
            {
                randomPercentage -= currentBehaviour.behaviours[i].percentage;
            }
        }
        return EnemyActions.Idle;
    }

    public void SetActionBehaviour(EnemyActions _action) {
        switch (_action) {
            case EnemyActions.Idle:
                stateMachine.ChangeState(Enemy_Idle.i);
                break;
            case EnemyActions.Move_Forward:
                stateMachine.ChangeState(Enemy_MoveForward.i);
                break;
            case EnemyActions.Move_Backward:
                stateMachine.ChangeState(Enemy_MoveBack.i);
                break;
            case EnemyActions.Move_Left:
                stateMachine.ChangeState(Enemy_MoveLeft.i);
                break;
            case EnemyActions.Move_Right:
                stateMachine.ChangeState(Enemy_MoveRight.i);
                break;
            case EnemyActions.Defending:
                stateMachine.ChangeState(Enemy_Defending.i);
                break;
            case EnemyActions.Dodge_Forward:
                stateMachine.ChangeState(Enemy_DodgingForward.i);
                break;
            case EnemyActions.Dodge_Left:
                stateMachine.ChangeState(Enemy_DodgingLeft.i);
                break;
            case EnemyActions.Dodge_Right:
                stateMachine.ChangeState(Enemy_DodgingRight.i);
                break;
            case EnemyActions.L:
                stateMachine.ChangeState(Enemy_L.i);
                break;
            case EnemyActions.LL:
                //stateMachine.ChangeState(enemy_.i);
                break;
            case EnemyActions.WL:
                //stateMachine.ChangeState(Enemy_WL.i);
                break;
            case EnemyActions.WLSL:
                //stateMachine.ChangeState(Enemy_WLSL.i);
                break;
        }
        actions = _action;
    }
    #endregion

    #region Animation
    protected void AnimController() {
        cbv.anim.SetBool("isDefending", cbv.isDefending);
        cbv.anim.SetBool("isDodging",cbv.isDodging);
        cbv.anim.SetFloat("Horizontal",direction.x);
        cbv.anim.SetFloat("Vertical", direction.z);
    }

    #endregion

}
