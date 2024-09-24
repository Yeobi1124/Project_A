using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringRope : MonoBehaviour
{
    public SpringJoint2D springJoint2D;
    public Transform anchor;
    public LineRenderer lineRenderer;
    public float distance;

    private void FixedUpdate() {
        springJoint2D.enabled = distance <= Vector3.Distance(transform.position, anchor.position);
    }

    private void OnDisable() {
    }

    public void InActive(){
        springJoint2D.enabled = false;
        gameObject.SetActive(false);
    }
    public void Draw(){
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, anchor.position);
    }

    public void Spring(){
        springJoint2D.enabled = true;
        springJoint2D.autoConfigureConnectedAnchor = false;
        springJoint2D.connectedBody = anchor.gameObject.GetComponent<Rigidbody2D>();

        distance = Vector3.Distance(anchor.position, transform.position);
        springJoint2D.distance = distance;
    }
}
