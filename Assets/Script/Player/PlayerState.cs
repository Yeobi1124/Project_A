using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool onGround; // 땅 위에 있는지, Animator와 관련
    public bool onAnchor; //Anchor 박혔는지
    public bool isTight; //줄이 팽팽한지
    public bool hasMuffler;
    

    //Animator
    Animator anim;

    //object For State Update
    Rigidbody2D rigid;

    RaycastHit2D hit;
    float raycastLength = 1f;

    public Anchor anchor;
    public RopePhysics springRope;

    private void Awake() {
        TryGetComponent(out anim);
        TryGetComponent(out rigid);
    }

    private void Update() {
        hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, Vector2.down * raycastLength);
        onGround = hit.collider;

        onAnchor = anchor.gameObject.activeSelf && anchor.state == Anchor.State.Fixed;

        isTight = springRope.springJoint2D.enabled;

        //Update Animation
        anim.SetBool("isRun", Mathf.Abs(rigid.velocity.x) > 0.1);
        anim.SetBool("onGround", onGround);
        anim.SetBool("isFall", rigid.velocity.y < 0);
    }
}
