using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value){
        Debug.Log(value);
    }

    void OnJump(InputValue value){
        Debug.Log("Jump");
        Debug.Log(value);
    }

    void OnMouse(InputValue value){
        Debug.Log("Mouse Click");
        Debug.Log(value);
    }
}
