using AIState;


public class Enemy_MoveLeft: State<EnemyBase> 
{
	private static Enemy_MoveLeft _i;

	private Enemy_MoveLeft()
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

public static Enemy_MoveLeft i
{
	get
{
	if(_i == null)
{
		new Enemy_MoveLeft();
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
		_owner.Move_Left();
	}
}
