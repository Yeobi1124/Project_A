using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mediator;

public class Player : MonoBehaviour
{
    public AbstractMediator mediator;

    [HideInInspector]
    public PlayerAttribute attribute;
    [HideInInspector]
    public PlayerState state;

    //Movement
    private MoveHorizion moveHorizion;
    private MoveRope moveRope;
    private Jump jump;
    private DashRope _dashRope;

    //기능 구현을 위한 변수들
    private Rigidbody2D rigid;

    private bool VecRemove; //Anchor 꽂을 때 튀어오르는 거 방지용 (이거 해도 뭔가 남아있음. 줄 길이도 발사, 고정 사이 길이보다 짧고)

    //임시 저장
    public Transform anchorTransform;

    private void Awake() {
        TryGetComponent(out attribute);
        TryGetComponent(out state);

        TryGetComponent(out moveHorizion);
        TryGetComponent(out moveRope);
        TryGetComponent(out jump);
        TryGetComponent(out _dashRope);

        TryGetComponent(out rigid);

        //Movement Init
        moveHorizion.Init(attribute.moveHorizionSpeed);
        jump.Init(attribute.jumpPower);
        moveRope.Init(attribute.ropeMovePower);
        _dashRope.Init(attribute.dashRopePower);
    }

    private void FixedUpdate() {
        switch(state.controltype){
            case PlayerState.Controltype.Ground:
            case PlayerState.Controltype.Air:
                rigid.freezeRotation = true;
                transform.rotation = Quaternion.identity;
                moveHorizion.UpdateAct();

                // VecRemove = true;
                break;
            case PlayerState.Controltype.Rope:
                rigid.freezeRotation = false;
                // if(VecRemove){
                //     rigid.velocity = new Vector2(0, 0);
                //     VecRemove = false;
                // }
                if(state.isTight)
                    moveRope.UpdateAct(anchorTransform.position);
                break;
            default:
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();

        moveHorizion.Act(value);
        moveRope.Act(value);
        _dashRope.SetDir((int)value);
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            jump.Act();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (state.controltype == PlayerState.Controltype.Rope)
            {
                _dashRope.Act(anchorTransform.position);
            }
        }
    }
}
