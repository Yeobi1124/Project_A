using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use SpringJoint2D
public class RopePhysics : MonoBehaviour
{
    private SpringJoint2D springJoint2D;
    public Rigidbody2D anchor;
    public Rigidbody2D rootAnchor;
    public float distance;
    public bool active = false;
    public bool isTight;

    private void Start() {
        springJoint2D = rootAnchor.GetComponent<SpringJoint2D>();

        if(springJoint2D == null){
            Debug.LogWarning("Root Anchor's SpringJoint2D is missing");
        }
    }

    private void FixedUpdate() {
        isTight = springJoint2D.distance < Vector3.Distance(rootAnchor.transform.position, anchor.transform.position);
        springJoint2D.enabled = active && isTight;
    }

    public void InActive(){
        active = false;

        springJoint2D.connectedBody = null;
    }

    public void Active(){
        active = true;

        //springJoint2D.autoConfigureConnectedAnchor = false;
        springJoint2D.connectedBody = anchor;

        distance = Vector3.Distance(anchor.position, rootAnchor.transform.position);
        //springJoint2D.distance = distance;
    }
}
