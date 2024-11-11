using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use SpringJoint2D
public class RopePhysics : MonoBehaviour
{
    public SpringJoint2D springJoint2D;
    public Rigidbody2D anchor;
    public float distance;
    public bool active = false;
    public bool isTight;

    private void FixedUpdate() {
        if(active){
            springJoint2D.enabled = distance <= Vector3.Distance(transform.position, anchor.position);
        }
    }

    public void InActive(){
        active = false;

        springJoint2D.enabled = false;
        springJoint2D.connectedBody = null;
    }

    public void Active(){
        active = true;

        springJoint2D.enabled = true;
        springJoint2D.autoConfigureConnectedAnchor = false;
        springJoint2D.connectedBody = anchor;

        distance = Vector3.Distance(anchor.position, transform.position);
        springJoint2D.distance = distance;
    }
}
