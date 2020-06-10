using AIState;


public class Enemy_MoveRight: State<EnemyBase> 
{
	private static Enemy_MoveRight _i;

	private Enemy_MoveRight()
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

public static Enemy_MoveRight i
{
	get
{
	if(_i == null)
{
		new Enemy_MoveRight();
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
		_owner.Move_Right();
	}
}
