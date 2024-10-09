using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class AI_Enemy01 : MonoBehaviour
{
    Rigidbody2D rigid;

    Selector rootNode;
    Transform target;

    //Temp
    float attackRange = 1f;
    float speed = 10f;

    private void Awake() {
        TryGetComponent(out rigid);

        rootNode = new Selector(
            new List<INode>(){
                new Sequence( //Find Player 아 이건 뒤로 밀려야 할 듯. 우선순위 낮으니까
                    new List<INode>(){
                        new Action(DetectPlayer),
                        new Action(MoveToPlayer)
                    }
                )
            }
        );
    }

    private void Update() {
        rootNode.Evaluate();
    }

    INode.State DetectPlayer(){
        Collider2D[] colli = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 1), 0, LayerMask.GetMask("Player"));
        
        //For Debug
        Debug.DrawLine(transform.position + new Vector3(-5, -0.5f), transform.position + new Vector3(5, -0.5f));
        Debug.DrawLine(transform.position + new Vector3(-5, 0.5f), transform.position + new Vector3(5, 0.5f));
        Debug.DrawLine(transform.position + new Vector3(-5, -0.5f), transform.position + new Vector3(-5, 0.5f));
        Debug.DrawLine(transform.position + new Vector3(5, -0.5f), transform.position + new Vector3(5, 0.5f));

        if(colli != null && colli.Length != 0){
            target = colli[0].transform;

            Debug.Log("Detect Success");
            return INode.State.Success;
        }

        target = null;

        return INode.State.Failure;
    }

    INode.State MoveToPlayer(){
        /*
        if (_detectedPlayer != null)
        {
            if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_meleeAttackRange * _meleeAttackRange))
            {
                return INode.ENodeState.ENS_Success;
            }

            transform.position = Vector3.MoveTowards(transform.position, _detectedPlayer.position, Time.deltaTime * _movementSpeed);

            return INode.ENodeState.ENS_Running;
        }

        return INode.ENodeState.ENS_Failure;
        */
        if(target == null)
            return INode.State.Failure;
        
        if(transform.position.magnitude < attackRange){
            return INode.State.Success;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        return INode.State.Success;
    }
}
