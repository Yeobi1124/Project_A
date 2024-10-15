using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player Property
    private PlayerAttribute attribute;
    private PlayerState state;

    //Rope
    public RopeController ropeController;

    //Movement
    private MoveHorizion moveHorizion;
    private MoveRope moveRope;
    private Jump jump;

    //기능 구현을 위한 변수들
    private Camera cam;
    private Rigidbody2D rigid;

    private bool VecRemove; //Anchor 꽂을 때 튀어오르는 거 방지용 (이거 해도 뭔가 남아있음. 줄 길이도 발사, 고정 사이 길이보다 짧고)

    private void Awake() {
        TryGetComponent(out attribute);
        TryGetComponent(out state);

        TryGetComponent(out ropeController);
        TryGetComponent(out moveHorizion);
        TryGetComponent(out moveRope);
        TryGetComponent(out jump);

        TryGetComponent(out rigid);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Movement Init
        moveHorizion.Init(rigid, attribute.moveHorizionSpeed);
        jump.Init(rigid, attribute.jumpPower);
        moveRope.Init(rigid, attribute.ropeMovePower);
    }

    private void FixedUpdate() {
        if(!state.onAnchor){
            rigid.freezeRotation = true;
            transform.rotation = Quaternion.identity;
            moveHorizion.UpdateAct();

            VecRemove = true;
        }
        else{
            rigid.freezeRotation = false;
            if(VecRemove){
                rigid.velocity = new Vector2(0, 0);
                VecRemove = false;
            }
            if(state.isTight)
                moveRope.UpdateAct(ropeController.anchor.gameObject.transform.position);
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();

        moveHorizion.Act(value);
        moveRope.Act(value);
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            jump.Act();
        }
    }

    public void OnShoot(InputAction.CallbackContext context){ //목도리 발사
        if(context.started){
            ropeController.Shoot(cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        else if(context.canceled){
            ropeController.Cancel();
        }
    }

    public void OnThrow(InputAction.CallbackContext context) //목도리 던지는거
    {
        if(context.started){

        }
    }
}
