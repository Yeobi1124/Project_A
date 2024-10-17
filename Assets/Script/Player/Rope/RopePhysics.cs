using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use SpringJoint2D
public class RopePhysics : MonoBehaviour
{
    public SpringJoint2D springJoint2D;
    private Transform anchor;
    public float distance;
    public bool active = false;

    private void FixedUpdate() {
        if(active){
            springJoint2D.enabled = distance <= Vector3.Distance(transform.position, anchor.position);
            //Debug.Log(Vector3.Distance(transform.position, anchor.position));
        }
    }

    public void Init(Transform anchor){
        this.anchor = anchor;
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
        springJoint2D.connectedBody = anchor.gameObject.GetComponent<Rigidbody2D>();

        distance = Vector3.Distance(anchor.position, transform.position);
        springJoint2D.distance = distance;
    }
}
