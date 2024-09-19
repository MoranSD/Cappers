using System;

namespace Utilities.BehaviourTree
{
    public sealed class SelectorNode : BehaviourNode, INodeWithChildren
    {
        private BehaviourNode[] childNodes;
        private int nodeIndex;

        public SelectorNode(params BehaviourNode[] behaviourNodes)
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
                    nodeIndex = i;
                    childNodes[nodeIndex].Enter();
                    return;
                }
                else if (childNodes[i] is INodeWithChildren nodeWithChildren)
                {
                    if (nodeWithChildren.Contains(node))
                    {
                        nodeIndex = i;
                        nodeWithChildren.MoveTo(node);
                        return;
                    }
                }
            }

            throw new Exception(node.GetType().ToString());
        }

        protected override void OnEnter()
        {
            if (childNodes.Length == 0)
                throw new System.Exception(GetType().ToString());

            nodeIndex = 0;
            childNodes[nodeIndex].Enter();
        }

        protected override void OnRun(float deltaTime)
        {
            var currentNode = childNodes[nodeIndex];

            if (currentNode.Status != NodeStatus.run)
            {
                if (currentNode.Status == NodeStatus.failure)
                {
                    currentNode.Exit();
                    nodeIndex++;

                    if (nodeIndex >= childNodes.Length) Stop(false);
                    else childNodes[nodeIndex].Enter();
                }
                else if (currentNode.Status == NodeStatus.success)
                {
                    currentNode.Exit();
                    Stop(true);
                }
            }
            else
            {
                currentNode.Run(deltaTime);
            }
        }

        protected override void OnExit()
        {
            if (Status != NodeStatus.run) return;

            var currentNode = childNodes[nodeIndex];
            currentNode.Exit();
        }
    }
}