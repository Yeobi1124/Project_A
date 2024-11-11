using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Jump : MonoBehaviour
{
    int power;
    Rigidbody2D _rigid;
    Rigidbody2D rigid{
        get{
            if(_rigid == null && !TryGetComponent(out _rigid)){
                Debug.LogWarning("Rigidbody2D is missing");
            }
            return _rigid;
        }
        set => _rigid = value;
    }

    public void Init(int power){
        this.power = power;
    }
    public void Act(){
        rigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
}
