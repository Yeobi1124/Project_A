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

    public void UpdateAct(Vector3 anchorPos){
        Vector3 forceDir = Quaternion.Euler(0, 0, -90) * (anchorPos - transform.position).normalized * dir * power;
        rigid.AddForce(forceDir,ForceMode2D.Force);

        Debug.DrawRay(transform.position, forceDir, Color.red);
        Debug.DrawRay(transform.position, anchorPos - transform.position, Color.green);
        
        Debug.Log(anchorPos);
        Debug.Log(transform.position);
    }

    public void Set(float value){
        dir = value;
    }
}
