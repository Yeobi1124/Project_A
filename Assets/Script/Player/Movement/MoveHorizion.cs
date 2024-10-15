using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizion : MonoBehaviour
{
    int speed;
    float dir; // dir < 0 Left, dir > 0 Right
    Rigidbody2D rigid;
    Vector2 tempVelocity;

    public void Init(Rigidbody2D rigid, int speed) {
        this.rigid = rigid;
        this.speed = speed;
    }

    public void Act(float value){
        dir = value;
    }

    public void UpdateAct(){
        if(rigid == null){
            Debug.LogWarning("Rigidbody2D is missing");
            return;
        }

        tempVelocity.y = rigid.velocity.y;
        tempVelocity.x = dir * speed;

        rigid.velocity = tempVelocity;
    }
}
