using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIState;

public class Enemy_Blade : EnemyBase
{
    
    public override void Start()
    {
        base.Start();
        stateMachine.ChangeState(Enemy_Idle.i);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Update();
    }
}
