using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerAttribute attribute;
    private PlayerState state;

    private RopeController ropeController;
    private MoveHorizion moveHorizion;
    private MoveRope moveRope;
    private Jump jump;

    private Camera cam;
    private Rigidbody2D rigid;

    private bool yVecRemove; //Anchor 꽂을 때 튀어오르는 거 방지용

    private void Awake() {
        TryGetComponent(out attribute);
        TryGetComponent(out state);

        TryGetComponent(out ropeController);
        TryGetComponent(out moveHorizion);
        TryGetComponent(out moveRope);
        TryGetComponent(out jump);

        TryGetComponent(out rigid);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate() {
        if(!state.onAnchor){
            rigid.freezeRotation = true;
            transform.rotation = Quaternion.identity;
            moveHorizion.UpdateAct();

            yVecRemove = true;
        }
        else{
            rigid.freezeRotation = false;
            if(yVecRemove){
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                yVecRemove = false;
            }
            if(state.isTight)
                moveRope.UpdateAct(ropeController.anchor.gameObject.transform.position);
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();

        moveHorizion.Set(value);
        moveRope.Set(value);
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            jump.Act();
        }
    }

    public void OnMouse(InputAction.CallbackContext context){
        if(context.started){
            ropeController.Shoot(cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        else if(context.canceled){
            ropeController.Cancel();
        }
    }
}
