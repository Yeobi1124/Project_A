using System.Collections.Generic;

public class MoveStateController : IPlayerStateController
{

    public IPlayerStateMachine stateMachine{get; private set;}
    public void Init(){}
    public void Update(){
        stateMachine.Update();
    }
}