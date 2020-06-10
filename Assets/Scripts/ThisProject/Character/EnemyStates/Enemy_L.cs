using AIState;


public class Enemy_L: State<EnemyBase> 
{
	private static Enemy_L _i;

	private Enemy_L()
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

public static Enemy_L i
{
	get
{
	if(_i == null)
{
		new Enemy_L();
}
	return _i;
}
	
}

    public override void StateEnter(EnemyBase _owner)
    {
        _owner.L_Enter();
    }

    public override void StateExit(EnemyBase _owner)
    {
        
    }

    public override void StateUpdate(EnemyBase _owner)
    {
        _owner.BackToIdle_AnimationCompeleted();
    }
}
