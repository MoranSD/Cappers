using System;

namespace Utilities.BehaviourTree
{
    public sealed class DoConditionNode : BehaviourNode
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