using AIState;


public class Enemy_Idle: State<EnemyBase> 
{
	private static Enemy_Idle _i;

	private Enemy_Idle()
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

public static Enemy_Idle i
{
	get
{
	if(_i == null)
{
		new Enemy_Idle();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
		_owner.IdleEnter();
    }

    public override void StateExit(EnemyBase _owner)
    {
    }

    public override void StateUpdate(EnemyBase _owner)
    {
		_owner.LookTarget();
	}
}
