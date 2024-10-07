using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController
{
    public IPlayerStateController moveStateMachine;
    // public IPlayerStateMachine combatStateMachine;
    // public IPlayerStateMachine ropeStateMachine;

    [Header("Condition")]
    public PlayerInputContext inputContext;

    public void Init()
    {
        moveStateMachine = new MoveStateController();
    }

    public void Update()
    {
        moveStateMachine.Update();
    }
}