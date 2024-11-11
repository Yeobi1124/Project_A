using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizion : MonoBehaviour
{
    int speed;
    float dir; // dir < 0 Left, dir > 0 Right
    Rigidbody2D _rigid;
    Rigidbody2D rigid{
        get{
            if(_rigid == null){
                if(!TryGetComponent(out _rigid)){
                    Debug.LogWarning("Rigidbody2D is missing!");
                }
            }
            return _rigid;
        }
        set => _rigid = value;
    }
    Vector2 tempVelocity;

    public void Init(int speed) {
        this.speed = speed;
    }

    public void Act(float value){
        dir = value;
    }

    public void UpdateAct(){
        tempVelocity.y = rigid.velocity.y;
        tempVelocity.x = dir * speed;

        rigid.velocity = tempVelocity;
    }
}
