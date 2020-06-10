using AIState;


public class Enemy_MoveForward: State<EnemyBase> 
{
	private static Enemy_MoveForward _i;

	private Enemy_MoveForward()
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

public static Enemy_MoveForward i
{
	get
{
	if(_i == null)
{
		new Enemy_MoveForward();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
		_owner.MovementEnter();
    }

    public override void StateExit(EnemyBase _owner)
    {
		_owner.StopMoving();
    }

	public override void StateUpdate(EnemyBase _owner)
	{
        if (!_owner.IsCloseTarget())
        {
            _owner.Move_Forward();
        }
        else
        {
            _owner.NextToTarget();
        }
    }
}
