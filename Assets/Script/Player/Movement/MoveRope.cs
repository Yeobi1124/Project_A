using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRope : MonoBehaviour
{
    public float power;
    Rigidbody2D rigid;
    float dir;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    public void UpdateAct(){
        rigid.AddForce(transform.right * dir * power,ForceMode2D.Force);
    }

    public void Set(float value){
        dir = value;
    }
}
