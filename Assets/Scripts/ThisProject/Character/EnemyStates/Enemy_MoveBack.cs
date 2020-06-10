using AIState;


public class Enemy_MoveBack: State<EnemyBase> 
{
	private static Enemy_MoveBack _i;

	private Enemy_MoveBack()
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

public static Enemy_MoveBack i
{
	get
{
	if(_i == null)
{
		new Enemy_MoveBack();
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
            _owner.Move_Backward();
        }
        else
        {
            _owner.NextToTarget();
        }
    }
}
