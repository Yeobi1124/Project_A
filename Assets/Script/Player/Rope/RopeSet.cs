using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSet : MonoBehaviour
{
    public Transform[] children;
    public GameObject player;
    private void Awake() {
        children = GetComponentsInChildren<Transform>();
    }

    private void OnDisable() {
        foreach(Transform child in children){
            if(child != transform)
                child.position = player.transform.position;
        }
    }
}
