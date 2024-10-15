using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    int power;
    Rigidbody2D rigid;

    public void Init(Rigidbody2D rigid, int power){
        this.rigid = rigid;
        this.power = power;
    }
    public void Act(){
        if(rigid == null){
            Debug.LogWarning("Rigidbody2D is missing");
            return;
        }

        rigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
}
