using AIState;


public class Enemy_DodgingLeft: State<EnemyBase> 
{
	private static Enemy_DodgingLeft _i;

	private Enemy_DodgingLeft()
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

public static Enemy_DodgingLeft i
{
	get
{
	if(_i == null)
{
		new Enemy_DodgingLeft();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
        _owner.Dodge_Left();
        _owner.DodgingEnter();
    }

    public override void StateExit(EnemyBase _owner)
    {
        _owner.DodgingExit();
    }

    public override void StateUpdate(EnemyBase _owner)
    {
        _owner.BackToIdle_AnimationCompeleted();
    }
}
