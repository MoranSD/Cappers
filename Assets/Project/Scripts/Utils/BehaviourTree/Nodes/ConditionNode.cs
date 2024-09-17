using System;

namespace Utilities.BehaviourTree
{
    public sealed class ConditionNode : BehaviourNode
    {
        private Func<bool> condition;
        public ConditionNode(Func<bool> condition)
        {
            this.condition = condition;
        }

        protected override void OnEnter()
        {
            bool? result = condition?.Invoke();

            Stop(result.HasValue ? result.Value : false);
        }
    }
}