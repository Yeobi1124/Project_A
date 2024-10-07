using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    public int speed;
    public int jumpPower;
    Rigidbody2D rigid;
    Vector2 velocity;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    private void FixedUpdate() {
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;
    }

    public void MoveHorizon(float value){
        velocity.x = value * speed;
    }

    public void Jump(){
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}