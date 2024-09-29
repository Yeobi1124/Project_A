using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeMovement : MonoBehaviour, IPlayerMovement
{
    public int power;
    Rigidbody2D rigid;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    // private void OnEnable() {
    //     rigid.freezeRotation = false;
    // }

    // private void OnDisable() {
    //     transform.rotation = Quaternion.identity ;
    //     rigid.freezeRotation = true;
    // }

    public void MoveHorizon(float value){
        rigid.AddForce(transform.right * value * power, ForceMode2D.Force);
    }

    public void Jump(){
    }
}