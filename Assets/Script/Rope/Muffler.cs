using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mediator;
using UnityEngine.Serialization;

public class Muffler : MonoBehaviour
{
    public AbstractMediator mediator;

    [Header("Owner")]
    public Rigidbody2D ownerJointPoint; // 어떤 Character가 가지는지

    [Header("Property")]
    public RopePhysics physics;
    public RopeVisual visual;
    public Anchor anchor;
    public Anchor rootAnchor; //Throw에 쓸 앵커

    [Header("Attribute")]
    public float range;

    // Rope State
    [SerializeField]
    private bool isPlayerOwned;
    private bool IsPlayerOwned
    {
        get => isPlayerOwned;
        set
        {
            isPlayerOwned = value;
            if(mediator != null){
                mediator.Notify(this, "isPlayerOwned", isPlayerOwned);
            }
        }
    }
    [SerializeField]
    private bool physicsActive;
    private bool PhysicsActive
    {
        get => physicsActive;
        set{
            physicsActive = value;
            if(mediator != null){
                mediator.Notify(this, "isActive", physicsActive);
            }
        }
    }
    [SerializeField]
    private bool isTight;
    private bool IsTight{
        get => isTight;
        set
        {
            isTight = value;
            if(mediator != null){
                mediator.Notify(this, "isTight", isTight);
            }
        }
    }

    private void Start() {


        FixedJoint2D fixedJoint2D = rootAnchor.GetComponent<FixedJoint2D>();
        if(fixedJoint2D != null){
            fixedJoint2D.connectedBody = ownerJointPoint;
        }
    }

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
                PhysicsActive = true;

                visual.Active();
                visual.segmentLength = (anchor.transform.position - rootAnchor.transform.position).magnitude / visual.segementCount;
                break;
            case Anchor.State.Fixed:
                break;
        }

        switch(rootAnchor.state){
            case Anchor.State.None:
            case Anchor.State.Flying:
            case Anchor.State.Fixed:
                break;
            case Anchor.State.Catch:
                rootAnchor.Fix();
                break;
            default:
                break;
        }

        // State Update
        if(IsTight != physics.isTight){ //set 연산이 mediator로 비용이 큰 연산이 되서 최적화용
            IsTight = physics.isTight;
        }
    }

    public void Shoot(Vector2 target){
        if(!IsPlayerOwned) anchor.collisionLayerMask = LayerMask.GetMask("Character");
        else anchor.collisionLayerMask = LayerMask.GetMask("Platform");

        anchor.gameObject.SetActive(true);
        anchor.transform.position = rootAnchor.transform.position;
        anchor.MoveTo(target);
    }
    public void ShootCancel(){
        anchor.gameObject.SetActive(false);
        
        physics.InActive();
        PhysicsActive = false;

        visual.InActive();
    }
    private void Throw(Vector2 target){
        rootAnchor.gameObject.SetActive(true);
        // rootAnchor.transform.position = transform.position;
        rootAnchor.MoveTo(target);
    }
    private void ThrowCancel(){
        rootAnchor.gameObject.SetActive(false);
        rootAnchor.transform.position = transform.position;
    }

    
    public void OnShoot(InputAction.CallbackContext context){ //목도리 발사
        if(context.started){
            if(IsPlayerOwned){
                rootAnchor.transform.position = ownerJointPoint.transform.position;
                rootAnchor.gameObject.SetActive(true);
                rootAnchor.GetComponent<FixedJoint2D>().enabled = true;
                
                rootAnchor.collisionLayerMask = LayerMask.GetMask();
            }
            
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if(context.canceled){
            ShootCancel();

            if(IsPlayerOwned){
                rootAnchor.gameObject.SetActive(false);
                rootAnchor.GetComponent<FixedJoint2D>().enabled = false;
            }
        }
    }

    public void OnThrow(InputAction.CallbackContext context) //목도리 던지는거
    {
        if(context.started){
            if(rootAnchor.state == Anchor.State.Fixed){
                rootAnchor.transform.position = ownerJointPoint.transform.position;
                rootAnchor.collisionLayerMask = LayerMask.GetMask();
                ThrowCancel();

                IsPlayerOwned = true;
            }
            else{
                rootAnchor.transform.position = ownerJointPoint.transform.position;
                rootAnchor.collisionLayerMask = LayerMask.GetMask("Platform");
                Throw(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                IsPlayerOwned = false;
            }
        }
    }
}
