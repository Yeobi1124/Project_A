using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirMovement : MonoBehaviour
{
    public int moveForce;
    Rigidbody2D rigid;

    private void Awake() {
        TryGetComponent(out rigid);
    }

    public void MoveHorizon(float value){
    }
}