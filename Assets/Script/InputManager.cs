using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player player;
    public RopeController rope;
    public Camera mainCamera;

    private void Awake() {
        
    }

    private void Update() {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
