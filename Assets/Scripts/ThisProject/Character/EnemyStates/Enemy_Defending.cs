using UnityEngine;
using AIState;


public class Enemy_Defending: State<EnemyBase> 
{
	private static Enemy_Defending _i;

	private Enemy_Defending()
{
		if(_i == null)
{
			_i = this;
}
		else
{
			return;
}

}

public static Enemy_Defending i
{
	get
{
	if(_i == null)
{
		new Enemy_Defending();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
        _owner.DefendingEnter();
    }

    public override void StateExit(EnemyBase _owner)
    {
        _owner.DefendingExit();
    }

    public override void StateUpdate(EnemyBase _owner)
    {
        _owner.LookTarget();

    }
}
