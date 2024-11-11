using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public enum State {None, Flying, Catch, Fixed}
    public State state;
    public int speed;
    public LayerMask collisionLayerMask;
    Rigidbody2D _rigid;
    Rigidbody2D rigid{
        get{
            if(!_rigid){
                if(!TryGetComponent(out _rigid))
                    Debug.LogWarning("Rigidbody2D is missing");
            }
            return _rigid;
        }
        set {_rigid = value;}
    }
    Vector2 dir;
    private void OnEnable() { state = State.None; }
    private void OnDisable() { state = State.None; }
    private void FixedUpdate() { if(state == State.Flying) rigid.velocity = dir * speed; }
    private void OnTriggerEnter2D(Collider2D other) {
        if((collisionLayerMask.value & (1 << other.gameObject.layer)) != 0){ //LayerMask.value가 Flag같은 애라 비트연산자 사용
            state = State.Catch;
        }
    }

    public void MoveTo(Vector2 target){
        state = State.Flying;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        dir = (target - (Vector2)transform.position).normalized;
    }

    public void Cancel(){
        state = State.None;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        dir = Vector2.zero;
    }
    
    public void Fix(){
        state = State.Fixed;
        rigid.bodyType = RigidbodyType2D.Kinematic; // 움직이는 플랫폼 같은거 생각하면 Static 보단 Kinematic
    }
}
