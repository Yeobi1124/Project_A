using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    // Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rigid;

    private void Awake() {
        // TryGetComponent(out anim);
        TryGetComponent(out sprite);
        TryGetComponent(out rigid);
    }
    private void FixedUpdate() {
        if(Math.Abs(rigid.velocity.x) > 0.1){
            sprite.flipX = rigid.velocity.x < 0;
        }

        // anim.SetBool("isRun", Math.Abs(rigid.velocity.x) > 0.1);
    }
}
