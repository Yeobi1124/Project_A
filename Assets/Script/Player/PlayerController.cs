using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerState playerState;
    public RopeController ropeController;
    Camera cam;

    void Awake()
    {
        TryGetComponent(out playerMovement);
        TryGetComponent(out playerState);
        //TryGetComponent(out ropeController);

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void OnMove(InputAction.CallbackContext context){
        if(context.started || context.canceled){
            playerMovement.MoveHorizon(context.ReadValue<float>());
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started && playerState.state.HasFlag(PlayerState.State.OnGround)){
            playerMovement.Jump();
        }
    }

    public void OnMouse(InputAction.CallbackContext context){
        if(context.started){
            playerState.state |= PlayerState.State.Grappling;
            ropeController.Shoot(cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        else if(context.canceled){
            playerState.state &= ~PlayerState.State.Grappling;
            ropeController.Cancel();
        }
    }
}
