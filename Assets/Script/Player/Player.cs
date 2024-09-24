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
    public SegementRope ropePool;

    float dir;
    Rigidbody2D rigid;
    HingeJoint2D hinge;

    Vector2 vecHorizion;
    Vector2 vecVertical;
    Camera cam;

    public bool isJump;
    public RopeController rope;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
        hinge = GetComponent<HingeJoint2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
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
            rope.Shoot(cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            
            // ropePool.gameObject.SetActive(true);
            // ropePool.transform.position = transform.position;

            // hinge.connectedBody = ropePool.segements[ropePool.segements.Length-1].GetComponent<Rigidbody2D>();
        }
        else if(context.canceled){
            // anchor.gameObject.SetActive(false);
            // ropePool.gameObject.SetActive(false);
            rope.Cancel();
        }
    }
}
