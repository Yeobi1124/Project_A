using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class AI_Enemy01 : MonoBehaviour
{
    private EnemyState state;
    private EnemyAttribute attribute;

    Rigidbody2D rigid;

    Selector rootNode;
    Transform target;

    //Temp
    public float attackRange = 5f;
    public float speed = 10f;

    private void Awake() {
        TryGetComponent(out state);
        TryGetComponent(out attribute);
        TryGetComponent(out rigid);

        InitAI();
    }

    private void Update() {
        rootNode.Evaluate();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

    #region AI
    void InitAI(){
        rootNode = new Selector(
            new List<INode>(){
                //Check Character is Dead
                new Action(() => state.isDead ? INode.State.Success : INode.State.Failure),
                //Check Character is Tied
                new Action(() => state.isTied ? INode.State.Success : INode.State.Failure),
                new Sequence( //Find Player 아 이건 뒤로 밀려야 할 듯. 우선순위 낮으니까
                    new List<INode>(){
                        new Action(DetectPlayer),
                        new Action(MoveToPlayer)
                    }
                )
            }
        );
    }

    INode.State DetectPlayer(){
        Collider2D[] colli = Physics2D.OverlapBoxAll(transform.position, attribute.detectRange, 0, LayerMask.GetMask("Player"));
        DebugTool.DrawRectangle(transform.position, attribute.detectRange, Color.green);

        if(colli != null && colli.Length != 0){
            target = colli[0].transform;

            return INode.State.Success;
        }

        target = null;

        return INode.State.Failure;
    }

    INode.State MoveToPlayer(){
        if(target == null)
            return INode.State.Failure;
        
        if((transform.position - target.position).magnitude < attackRange){
            return INode.State.Success;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed); //얘 공중까지 쫓아와서 바꿔야됨

        return INode.State.Success;
    }
    #endregion
}
