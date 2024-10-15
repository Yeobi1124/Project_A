using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRope : MonoBehaviour
{
    int power;
    float dir;
    Rigidbody2D rigid;

    public void Init(Rigidbody2D rigid, int power) {
        this.rigid = rigid;
        this.power = power;
    }

    public void Act(float value){
        dir = value;
    }

    public void UpdateAct(Vector3 anchorPos){
        Vector3 forceDir = Quaternion.Euler(0, 0, -90) * (anchorPos - transform.position).normalized * dir * power;
        rigid.AddForce(forceDir,ForceMode2D.Force);

        Debug.DrawRay(transform.position, forceDir, Color.red);
        Debug.DrawRay(transform.position, anchorPos - transform.position, Color.green);
    }
}
