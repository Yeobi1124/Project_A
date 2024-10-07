using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int jumpPower;

    Rigidbody2D rigid;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    public void Act(){
        if(rigid == null){
            Debug.LogWarning("Rigidbody2D is missing");
            return;
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
