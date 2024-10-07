using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizion : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    float dir; // dir < 0 Left, dir > 0 Right
    Vector2 nextVelocity;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    public void UpdateAct(){
        if(rigid == null){
            Debug.LogWarning("Rigidbody2D is missing");
            return;
        }

        nextVelocity.y = rigid.velocity.y;
        nextVelocity.x = dir * speed;

        rigid.velocity = nextVelocity;
    }

    public void Set(float value){
        dir = value;
    }
}
