using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public enum State {Idle, Running, Success, Failure, Fix};
    public State currentState;
    public int speed;
    Rigidbody2D rigid;
    Vector2 dir;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();

        currentState = State.Idle;
    }

    private void OnEnable() {
        currentState = State.Idle;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
            currentState = State.Success;
        }
    }

    public void Fix(){
        currentState = State.Fix;

        rigid.bodyType = RigidbodyType2D.Static;
    }

    public void Act(Vector2 dir){
        this.dir = dir;
        rigid.bodyType = RigidbodyType2D.Dynamic;

        currentState = State.Running;
    }

    private void FixedUpdate() {
        if(rigid.bodyType != RigidbodyType2D.Static)
            rigid.velocity = dir * speed;
    }
}
