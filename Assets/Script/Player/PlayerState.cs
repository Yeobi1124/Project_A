using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Core States")]
    public Controltype controltype;

    [Header("Basic States")]
    public bool onGround; // 땅 위에 있는지, Animator와 관련

    [Header("Rope States")] // Rope States의 경우 대부분 Player State 밖에서 처리할 듯. 의존성 문제 때문에 끊어놔서 여기서 확인할 수 없음.
    public bool hasMuffler; // Mediator에서 관리
    public bool isMufflerActive; // Mediator에서 관리
    public bool isTight; // 줄이 팽팽한지, RopeController, Mediator에서 관리
    

    public enum Controltype { Ground, Air, Rope }
    //Animator
    Animator anim;

    //object For State Update
    Rigidbody2D rigid;
    float raycastLength = 1f;

    private void Awake() {
        TryGetComponent(out anim);
        TryGetComponent(out rigid);
    }

    private void Update() {
        UpdateOnGround();
        UpdateControltype();
        UpdateAnimation();
    }

    private void UpdateOnGround(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, Vector2.down * raycastLength);
        onGround = hit.collider;
    }

    private void UpdateControltype(){
        if(onGround) controltype = Controltype.Ground;
        else if(hasMuffler && isMufflerActive && isTight) controltype = Controltype.Rope;
        else controltype = Controltype.Air;
    }

    private void UpdateAnimation(){
        anim.SetBool("isRun", Mathf.Abs(rigid.velocity.x) > 0.1);
        anim.SetBool("onGround", onGround);
        anim.SetBool("isFall", rigid.velocity.y < 0);
    }
}
