using System.Collections.Generic;

public class MoveGroundStateMachine : IPlayerStateMachine
{
    public IPlayerStateTemp state {get;}
    private Dictionary<string, IPlayerStateTemp> stateDict;

    public void Init(){
        stateDict.Add("Idle", new MoveGroundIdleState());
    }
    public void Enter(PlayerInputContext context){}
    public void Update(){}
    public void Exit(){}
}