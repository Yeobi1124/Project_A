using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool onGround;
    public bool onAnchor; //Anchor 박혔는지
    public bool isTight; //줄이 팽팽한지
    
    RaycastHit2D hit;
    float raycastLength = 1f;

    public Anchor anchor;
    public SpringRope springRope;

    private void Update() {
        hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platform"));
        onGround = hit.collider;

        onAnchor = anchor.gameObject.activeSelf && anchor.currentState == Anchor.State.Fix;

        isTight = springRope.springJoint2D.enabled;
    }
}
