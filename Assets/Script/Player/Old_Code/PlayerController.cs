using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerRopeMovement playerRopeMovement;
    PlayerStateOld playerState;
    public RopeController ropeController;
    Camera cam;

    Animator animator;
    SpriteRenderer spriteRenderer;

    Dictionary<string, IPlayerMovement> movementDict; //나중에 movement 이런 식으로 정리

    void Awake()
    {
        //TryGetComponent(out playerMovement);
        TryGetComponent(out playerState);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
        //TryGetComponent(out ropeController);

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void OnMove(InputAction.CallbackContext context){
        var value = context.ReadValue<float>();

        //for test
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if(ropeController.anchor.currentState != Anchor.State.Fix){
            //for test
            transform.rotation = Quaternion.identity;
            rigid.freezeRotation = true;

            playerMovement.MoveHorizon(value);
        }
        else{
            rigid.freezeRotation = false;
            playerRopeMovement.MoveHorizon(value);
        }
        animator.SetBool("isRun", value!=0);

        if(value != 0)
            spriteRenderer.flipX = value < 0;
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started && playerState.state.HasFlag(PlayerStateOld.State.OnGround)){
            playerMovement.Jump();
        }
    }

    public void OnMouse(InputAction.CallbackContext context){
        if(context.started){
            playerState.state |= PlayerStateOld.State.Grappling;
            ropeController.Shoot(cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        else if(context.canceled){
            playerState.state &= ~PlayerStateOld.State.Grappling;
            ropeController.Cancel();
        }
    }
}
