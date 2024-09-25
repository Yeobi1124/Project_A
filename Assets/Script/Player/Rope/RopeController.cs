using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    //public IRope rope;
    public SpringRope rope;
    public Anchor anchor;
    public int maxLength;

    [Header("Test")]
    public VerletIntergration verlet;

    private void Update() {
        if(anchor.currentState == Anchor.State.Success){
            rope.Spring();
            anchor.Fix();

            verlet.gameObject.SetActive(true);
            verlet.segmentLength = (anchor.transform.position - transform.position).magnitude / verlet.segementCount;
        }
    }

    private void FixedUpdate() {
        //rope.Draw();
    }

    public void Shoot(Vector2 dir){
        anchor.gameObject.SetActive(true);
        anchor.transform.position = transform.position;
        anchor.Act(dir);

        rope.gameObject.SetActive(true);
    }
    
    public void Cancel(){
        anchor.gameObject.SetActive(false);
        
        rope.InActive();

        //test
        verlet.gameObject.SetActive(false);
    }
}
