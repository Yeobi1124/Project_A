using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    // 소유자 지정 방법 생각
    public Rigidbody2D owner; // Character가 가지고 있으면 Character의 Rigidbody. Platform에 박혀있으면 null로
    public Rigidbody2D defaultOwner;

    public RopePhysics physics;
    public RopeVisual visual;
    public Anchor anchor;
    public Anchor rootAnchor; //Throw에 쓸 앵커
    public float range;

    private void Update() {
        DebugTool.DrawCircle(rootAnchor.transform.position, range); //범위 표시

        switch(anchor.state){
            case Anchor.State.None:
                break;
            case Anchor.State.Flying:
                if((anchor.transform.position - rootAnchor.transform.position).magnitude > range){
                    ShootCancel();
                }
                break;
            case Anchor.State.Catch:
                anchor.Fix();
                
                physics.Active();

                visual.Active();
                visual.segmentLength = (anchor.transform.position - rootAnchor.transform.position).magnitude / visual.segementCount;
                break;
            case Anchor.State.Fixed:
                break;
        }

        if(rootAnchor.state == Anchor.State.Catch){
            rootAnchor.Fix();
        }
    }

    public void Shoot(Vector2 target){
        if(owner != null) anchor.collisionLayerMask = LayerMask.GetMask("Character");
        else anchor.collisionLayerMask = LayerMask.GetMask("Platform");

        anchor.gameObject.SetActive(true);
        anchor.transform.position = rootAnchor.transform.position;
        anchor.MoveTo(target);
    }
    public void ShootCancel(){
        anchor.gameObject.SetActive(false);
        
        physics.InActive();
        visual.InActive();
    }
    public void ToggleThrow(Vector2 target){
        if(rootAnchor.state == Anchor.State.Fixed){
            ThrowCancel();

            owner = defaultOwner;
        }
        else{
            Throw(target);

            owner = null;
        }
    }
    private void Throw(Vector2 target){
        rootAnchor.gameObject.SetActive(true);
        rootAnchor.transform.position = transform.position;
        rootAnchor.MoveTo(target);
    }
    private void ThrowCancel(){
        rootAnchor.gameObject.SetActive(false);
        rootAnchor.transform.position = transform.position;
    }
}
