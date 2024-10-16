using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public SpringRope rope;
    public Anchor anchor;
    public Anchor ownAnchor; //Throw에 쓸 앵커
    public VerletIntergration verlet;
    public int maxLength;

    private void Start() {
        rope.Init(anchor.transform);
        verlet.Init(anchor.transform, ownAnchor.transform);
    }

    private void Update() {
        if(anchor.currentState == Anchor.State.Success){
            rope.Spring();
            anchor.Fix();

            verlet.gameObject.SetActive(true);
            verlet.Active();
            verlet.segmentLength = (anchor.transform.position - transform.position).magnitude / verlet.segementCount;
        }
    }

    public void Shoot(Vector2 target){
        anchor.gameObject.SetActive(true);
        anchor.transform.position = transform.position;
        anchor.Act(target);

        rope.gameObject.SetActive(true);
    }

    public void Throw(Vector2 target){
        ownAnchor.gameObject.SetActive(true);
        ownAnchor.transform.position = transform.position;
        ownAnchor.Act(target);

        rope.gameObject.SetActive(true);
    }
    
    public void Cancel(){
        anchor.gameObject.SetActive(false);
        
        rope.InActive();

        //test
        verlet.gameObject.SetActive(false);
    }
}
