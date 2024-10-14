using UnityEngine;

public class NodeStateMachine{
    public LeafStateMachine currentStateMachine;

    public void SetState(LeafStateMachine stateMachine){
        if(stateMachine == null){
            Debug.Log("SetState can not set null");
            return;
        }
        
        if(currentStateMachine != stateMachine){
            currentStateMachine.Exit();
            currentStateMachine = stateMachine;
            currentStateMachine.Enter();
        }
    }

    public void Update(){
        currentStateMachine?.Update();
    }
}

public class LeafStateMachine : StateMachine
{
    public void Enter(){}
    public void Exit(){}
}

public class StateMachine
{
    public IState currentState;

    public void SetState(IState state){
        if(state == null){
            Debug.Log("SetState can not set null");
            return;
        }
        
        if(currentState != state){
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }

    public void Update(){
        currentState?.Update();
    }
}

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
}