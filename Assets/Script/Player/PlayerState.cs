using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool onGround;
    public bool onAnchor;
    
    RaycastHit2D hit;
    float raycastLength = 1f;

    public Anchor anchor;

    private void Update() {
        hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platform"));
        onGround = hit.collider;

        onAnchor = anchor.gameObject.activeSelf && anchor.currentState == Anchor.State.Fix;
    }
}
