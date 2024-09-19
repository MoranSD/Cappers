using System;

namespace Utilities.BehaviourTree
{
    public sealed class DoConditionNode : BehaviourNode, INodeWithChildren
    {
        private Func<bool> condition;

        private BehaviourNode successNode;
        private BehaviourNode failureNode;

        private BehaviourNode targetNode;

        public DoConditionNode(Func<bool> condition, BehaviourNode successNode, BehaviourNode failureNode)
        {
            this.condition = condition;
            this.successNode = successNode;
            this.failureNode = failureNode;
        }

        public bool Contains(BehaviourNode node)
        {
            if (node == successNode)
                return true;

            if (node == failureNode)
                return true;

            if (successNode is INodeWithChildren successNodeWithChilden)
                if (successNodeWithChilden.Contains(node))
                    return true;

            if (failureNode is INodeWithChildren failureNodeWithChilden)
                if (failureNodeWithChilden.Contains(node))
                    return true;

            return false;
        }

        public void MoveTo(BehaviourNode node)
        {
            if (Contains(node) == false)
                throw new Exception($"{node.GetType().ToString()}");

            if (TryEnterNodeWithChilden(successNode)) return;
            else if (TryEnterNodeWithChilden(failureNode)) return;
            else if (TryEnterConditionNode(successNode)) return;
            else if (TryEnterConditionNode(failureNode)) return;
            else throw new Exception(node.GetType().ToString());

            bool TryEnterConditionNode(BehaviourNode conditionNode)
            {
                if(conditionNode == node)
                {
                    targetNode = conditionNode;
                    targetNode.Enter();
                    return true;
                }

                return false;
            }
            bool TryEnterNodeWithChilden(BehaviourNode parentNode)
            {
                if (parentNode is INodeWithChildren nodeWithChilden)
                {
                    if (nodeWithChilden.Contains(node))
                    {
                        targetNode = parentNode;
                        nodeWithChilden.MoveTo(node);
                        return true;
                    }
                }

                return false;
            }
        }

        protected override void OnEnter()
        {
            bool? result = condition?.Invoke();

            targetNode = (result.HasValue ? result.Value : false) ? successNode : failureNode;
            targetNode.Enter();
        }

        protected override void OnRun(float deltaTime)
        {
            if (targetNode.Status != NodeStatus.run)
            {
                if (targetNode.Status == NodeStatus.success)
                {
                    targetNode.Exit();
                    Stop(true);
                }
                else if (targetNode.Status == NodeStatus.failure)
                {
                    targetNode.Exit();
                    Stop(false);
                }
            }
            else
            {
                targetNode.Run(deltaTime);
            }
        }

        protected override void OnExit() => targetNode.Exit();
    }
}