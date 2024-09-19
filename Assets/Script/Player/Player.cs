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
    public Anchor anchor;
    public RopeSet rope;

    float dir;
    Rigidbody2D rigid;
    HingeJoint2D hingeJoint;

    Vector2 vecHorizion;
    Vector2 vecVertical;

    public bool isJump;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //임시
        vecVertical = rigid.velocity;
        vecVertical.x = 0;
    }

    private void FixedUpdate() {
        //rigid.MovePosition(transform.position + speed * dir * Vector3.right * Time.fixedDeltaTime);
        //rigid.velocity = speed * dir * Vector2.right;
        rigid.velocity = vecHorizion + vecVertical; //임시

        if(isJump){
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        if(context.started || context.canceled){
            dir = context.ReadValue<float>();
            vecHorizion = speed * dir * Vector2.right;
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started)
            isJump = true;
    }

    public void OnMouse(InputAction.CallbackContext context){
        if(context.started){
            anchor.gameObject.SetActive(true);
            anchor.transform.position = transform.position;
            anchor.Act();
            
            rope.gameObject.SetActive(true);
            rope.transform.position = transform.position;

            hingeJoint.connectedBody = rope.children[rope.children.Length-1].GetComponent<Rigidbody2D>();
        }
        else if(context.canceled){
            anchor.gameObject.SetActive(false);
            rope.gameObject.SetActive(false);
        }
    }
}
