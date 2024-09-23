using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegements : MonoBehaviour
{
    public Transform[] segements;
    private void Awake() {
        segements = GetComponentsInChildren<Transform>();
    }

    public void AllSetActive(bool value){
        foreach(Transform child in segements){
            if(child != transform)
                child.gameObject.SetActive(value);
        }
    }
}
