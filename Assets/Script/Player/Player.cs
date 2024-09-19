using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class Player : MonoBehaviour
{
    public int speed;
    public int jumpPower;
    float dir;
    Rigidbody2D rigid;

    Vector2 vecHorizion;
    Vector2 vecVertical;

    public bool isJump;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        //rigid.MovePosition(transform.position + speed * dir * Vector3.right * Time.fixedDeltaTime);
        //rigid.velocity = speed * dir * Vector2.right;
        rigid.velocity = vecHorizion + vecVertical;

        if(isJump){
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }
    }

    void OnMove(InputValue value){
        dir = value.Get<float>();
        vecHorizion = speed * dir * Vector2.right;
    }

    void OnJump(InputValue value){
        Debug.Log("Jump");
        isJump = true;
    }

    void OnMouse(InputValue value){
        Debug.Log("Mouse Click");
    }
}
