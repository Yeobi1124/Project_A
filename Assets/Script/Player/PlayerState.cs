using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerState : MonoBehaviour
{
    [System.Flags]
    public enum State { //나중에 용어 정리 한번 ㄱ
        None = 0,
        OnGround = 1 << 0,
        Jumping = 1 << 1,
        Grappling = 1 << 2,
    };

    public State state;

    RaycastHit2D hit2D;

    private void FixedUpdate() {
        hit2D = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if(hit2D.collider != null && hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Platform")){
            state |= State.OnGround;
            state &= ~State.Jumping;
        }
        else{
            state &= ~State.OnGround;
        }
    }
}
