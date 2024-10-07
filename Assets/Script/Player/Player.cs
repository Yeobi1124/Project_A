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
        }
        else{
            rigid.freezeRotation = false;
            moveRope.UpdateAct();
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();

        if(!state.onAnchor){
            moveHorizion.Set(value);
        }
        else{
            moveRope.Set(value);
        }
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
