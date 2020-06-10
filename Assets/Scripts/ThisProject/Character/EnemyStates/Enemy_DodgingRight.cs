using AIState;


public class Enemy_DodgingRight: State<EnemyBase> 
{
	private static Enemy_DodgingRight _i;

	private Enemy_DodgingRight()
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

public static Enemy_DodgingRight i
{
	get
{
	if(_i == null)
{
		new Enemy_DodgingRight();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
        _owner.Dodge_Right();
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
