using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public int speed;
    Rigidbody2D rigid;
    Vector2 dir;
    Camera cam;

    public GameObject player; //temp

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
            rigid.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Act(){
        dir = (cam.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
        rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate() {
        if(rigid.bodyType != RigidbodyType2D.Static)
            rigid.velocity = dir * speed;
    }
}
