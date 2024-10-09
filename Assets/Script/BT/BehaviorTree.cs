using System;
using System.Collections.Generic;

namespace BehaviorTree
{
    interface INode
    {
        enum State{ Running, Success, Failure}
        public State Evaluate();
    }
    
    class Selector : INode
    {
        List<INode> _childs;

        public Selector(List<INode> childs){
            _childs = childs;
        }

        public INode.State Evaluate()
        {
            if(_childs == null || _childs.Count == 0)
                return INode.State.Failure;
            
            foreach(INode child in _childs){
                switch(child.Evaluate()){
                    case INode.State.Running:
                        return INode.State.Running;
                    case INode.State.Success:
                        return INode.State.Success;
                    case INode.State.Failure:
                        continue;
                }
            }

            return INode.State.Failure;
        }
    }

    class Sequence : INode
    {
        List<INode> _childs;

        public Sequence(List<INode> childs){
            _childs = childs;
        }

        public INode.State Evaluate(){
            if(_childs ==null || _childs.Count == 0 )
                return INode.State.Failure;

            foreach(INode child in _childs){
                switch(child.Evaluate()){
                    case INode.State.Running:
                        return INode.State.Running;
                    case INode.State.Success:
                        continue;
                    case INode.State.Failure:
                        return INode.State.Failure;
                }
            }

            return INode.State.Success;
        }
    }

    class Action : INode
    {
        Func<INode.State> _onUpdate = null;

        public Action(Func<INode.State> onUpdate){
            _onUpdate = onUpdate;
        }

        public INode.State Evaluate(){
            return _onUpdate?.Invoke() ?? INode.State.Failure;
        }
    }
}