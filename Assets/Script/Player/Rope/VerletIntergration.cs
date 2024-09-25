using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletIntergration : MonoBehaviour
{
    LineRenderer lineRenderer;
    public int segementCount;
    public int constraitLoop;
    public float lineWidth;
    public float segmentLength;
    public Transform firstAnchor;
    public Transform secondAnchor;
    public Vector2 gravity = new Vector2(0, 9.81f);

    private List<Segement> segements;

    public class Segement{
        public Vector2 prevPos;
        public Vector2 currPos;
        public Vector2 velocity;

        public Segement(Vector2 _pos){
            currPos = _pos;
            prevPos = _pos;
            velocity = Vector2.zero;
        }
    }

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        segements = new List<Segement>();

        Vector2 segementPos = firstAnchor.position;
        for(int i=0 ;i<segementCount;i++){
            segements.Add(new Segement(segementPos));
            segementPos.y -= segmentLength;
        }
    }

    private void FixedUpdate() {
        UpdateSegements();
        for(int i=0;i<constraitLoop;i++)
            ApplyConstraint();
        DrawRope();
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

    private void DrawRope(){
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        lineRenderer.positionCount = segementCount;
        for(int i=0;i<segementCount;i++){
            lineRenderer.SetPosition(i, segements[i].currPos);
        }
    }
}
