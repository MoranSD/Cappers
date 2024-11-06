using System;
using System.Linq;

namespace Utilities.BehaviourTree
{
    public sealed class ParallelNode : BehaviourNode, INodeWithChildren
    {
        private BehaviourNode[] childNodes;
        public ParallelNode(params BehaviourNode[] behaviourNodes)
        {
            childNodes = behaviourNodes;
        }

        public bool Contains(BehaviourNode node)
        {
            for (int i = 0; i < childNodes.Length; i++)
            {
                if (childNodes[i] == node)
                {
                    return true;
                }
                else if (childNodes[i] is INodeWithChildren nodeWithChildren)
                {
                    if (nodeWithChildren.Contains(node))
                        return true;
                }
            }

            return false;
        }

        public void MoveTo(BehaviourNode node)
        {
            if (Contains(node) == false)
                throw new Exception($"{node.GetType().ToString()}");

            for (int i = 0; i < childNodes.Length; i++)
            {
                if (childNodes[i] == node)
                {
                    OnEnter();
                    return;
                }
                else if (childNodes[i] is INodeWithChildren nodeWithChildren && nodeWithChildren.Contains(node))
                {
                    EnterAllExcept(childNodes[i]);
                    nodeWithChildren.MoveTo(node);
                    return;
                }
            }

            throw new Exception(node.GetType().ToString());

            void EnterAllExcept(BehaviourNode exceptNode)
            {
                foreach (var enterNode in childNodes)
                {
                    if (enterNode == exceptNode) continue;
                    enterNode.Enter();
                }
            }
        }

        protected override void OnEnter()
        {
            if (childNodes.Length == 0)
                throw new System.Exception(GetType().ToString());

            for (int i = 0; i < childNodes.Length; i++)
                childNodes[i].Enter();
        }

        protected override void OnRun(float deltaTime)
        {
            for (int i = 0; i < childNodes.Length; i++)
            {
                var node = childNodes[i];

                if(node.Status != NodeStatus.run)
                {
                    if(node.Status == NodeStatus.failure)
                    {
                        node.Exit();

                        foreach(var otherNode in childNodes)
                        {
                            if (otherNode == node) continue;
                            if (otherNode.Status == NodeStatus.idle) continue;

                            node.Exit();
                        }

                        Stop(false);
                    }
                    else if(node.Status == NodeStatus.success)
                    {
                        node.Exit();

                        if(IsAllIdle())
                            Stop(true);
                    }
                }
                else
                {
                    node.Run(deltaTime);
                }
            }
        }

        protected override void OnExit()
        {
            foreach (var node in childNodes)
            {
                if (node.Status != NodeStatus.run) continue;

                node.Exit();
            }
        }

        private bool IsAllIdle() => childNodes.All(x => x.Status == NodeStatus.idle);
    }
}