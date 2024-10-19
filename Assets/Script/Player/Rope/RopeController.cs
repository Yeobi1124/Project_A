using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    // 소유자 지정 방법 생각
    public RopePhysics physics;
    public Anchor anchor;
    public Anchor ownAnchor; //Throw에 쓸 앵커
    public RopeVisual visual;
    public float range;

    public bool isPlayerOwn;

    private void Start() {
        physics.Init(anchor.transform);
        visual.Init(anchor.transform, ownAnchor.transform);
    }

    private void Update() {
        DebugTool.DrawCircle(ownAnchor.transform.position, range); //범위 표시

        switch(anchor.state){
            case Anchor.State.None:
                break;
            case Anchor.State.Flying:
                if((anchor.transform.position - ownAnchor.transform.position).magnitude > range){
                    ShootCancel();
                }
                break;
            case Anchor.State.Catch:
                anchor.Fix();
                
                physics.Active();

                visual.Active();
                visual.segmentLength = (anchor.transform.position - ownAnchor.transform.position).magnitude / visual.segementCount;
                break;
            case Anchor.State.Fixed:
                break;
        }

        if(ownAnchor.state == Anchor.State.Catch){
            ownAnchor.Fix();
        }
    }

    public void Shoot(Vector2 target){
        if(!isPlayerOwn) anchor.collisionLayerMask = LayerMask.GetMask("Player", "Enemy");
        else anchor.collisionLayerMask = LayerMask.GetMask("Platform");

        anchor.gameObject.SetActive(true);
        anchor.transform.position = ownAnchor.transform.position;
        anchor.MoveTo(target);
    }
    public void ShootCancel(){
        anchor.gameObject.SetActive(false);
        
        physics.InActive();
        visual.InActive();
    }
    public void ToggleThrow(Vector2 target){
        if(ownAnchor.state == Anchor.State.Fixed){
            ThrowCancel();

            //Test
            isPlayerOwn = true;
        }
        else{
            Throw(target);

            //Test
            isPlayerOwn = false;
        }
    }
    private void Throw(Vector2 target){
        ownAnchor.gameObject.SetActive(true);
        ownAnchor.transform.position = transform.position;
        ownAnchor.MoveTo(target);
    }
    private void ThrowCancel(){
        ownAnchor.gameObject.SetActive(false);
        ownAnchor.transform.position = transform.position;
    }
}
