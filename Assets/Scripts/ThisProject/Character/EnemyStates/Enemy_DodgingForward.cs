using AIState;


public class Enemy_DodgingForward: State<EnemyBase> 
{
	private static Enemy_DodgingForward _i;

	private Enemy_DodgingForward()
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

public static Enemy_DodgingForward i
{
	get
{
	if(_i == null)
{
		new Enemy_DodgingForward();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
        _owner.Dodge_Forward();
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
