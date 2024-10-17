using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use Verlet Integration
public class RopeVisual : MonoBehaviour
{
    public bool active = false;
    public int segementCount;
    public int constraitLoop;
    public float lineWidth;
    public float segmentLength;
    public Transform firstAnchor;
    public Transform secondAnchor;
    public Vector2 gravity = new Vector2(0, 9.81f);

    private LineRenderer _lineRenderer;
    private LineRenderer lineRenderer{get{
        if(!_lineRenderer){
            if(!TryGetComponent(out _lineRenderer))
                Debug.LogWarning("LineRenderer is missing");
        }
        return _lineRenderer;
    }}
    private List<Segment> _segements;
    private List<Segment> segements{
        get{
            if(_segements == null) _segements = new List<Segment>();
            return _segements;
        } set{ _segements = value;}}

    private class Segment{
        public Vector2 prevPos;
        public Vector2 currPos;
        public Vector2 velocity;

        public Segment(Vector2 _pos){
            currPos = _pos;
            prevPos = _pos;
            velocity = Vector2.zero;
        }
    }

    public void Init(Transform firstAnchor, Transform secondAnchor){
        this.firstAnchor = firstAnchor;
        this.secondAnchor = secondAnchor;
        
        Vector2 segementPos = firstAnchor.position;
        for(int i=0 ;i<segementCount;i++){
            segements.Add(new Segment(segementPos));
            segementPos.y -= segmentLength;
        }
    }

    public void Active() {
        active = true;
        for(int i=0;i<segementCount;i++){
            segements[i].currPos = transform.position;
            segements[i].prevPos = transform.position;
            segements[i].velocity = Vector2.zero;
        }
    }

    public void InActive(){
        active = false;
    }

    private void FixedUpdate() {
        UpdateSegements();
        for(int i=0;i<constraitLoop;i++)
            ApplyConstraint();

        if(active) DrawRope();
        else EraseRope();
    }

    private void UpdateSegements(){
        for(int i=0;i<segementCount;i++){
            segements[i].velocity = segements[i].currPos - segements[i].prevPos;
            segements[i].prevPos = segements[i].currPos;
            segements[i].currPos += gravity * Time.fixedDeltaTime * Time.fixedDeltaTime; //가속도라 두번 곱해주는 듯? 좀 그렇네
            segements[i].currPos += segements[i].velocity;
        }
    }

    private void ApplyConstraint(){
        segements[0].currPos = firstAnchor.position;
        segements[segementCount-1].currPos = secondAnchor.position;
        //여기 고정점 추가하면 될 듯
        for(int i=0;i<segementCount-1;i++){
            float distance = (segements[i].currPos - segements[i+1].currPos).magnitude;
            float diff = segmentLength - distance; //현재 거리 - 자연 길이
            Vector2 dir = (segements[i+1].currPos - segements[i].currPos).normalized;
            Vector2 movement = dir * diff;
            
            if(i==0){
                segements[i+1].currPos += movement;
            }
            else{//다시 살펴볼 필요 있음
                segements[i].currPos -= movement * 0.5f;
                if(i!=segementCount-1)
                    segements[i+1].currPos += movement * 0.5f;
            }
        }
    }
    private void EraseRope(){
        lineRenderer.positionCount = 0;
    }
    private void DrawRope(){
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        lineRenderer.positionCount = segementCount;
        for(int i=0;i<segementCount;i++){
            lineRenderer.SetPosition(i, segements[i].currPos);
        }
    }
}
