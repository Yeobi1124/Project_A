using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public RopeSegements ropeSegements;
    public Anchor anchor;
    public int maxLength;

    public void Shoot(Vector2 dir){
        anchor.gameObject.SetActive(true);
        anchor.transform.position = transform.position;
        anchor.Act(dir);
    }
    
    public void Cancel(){
        anchor.gameObject.SetActive(false);
    }
}
