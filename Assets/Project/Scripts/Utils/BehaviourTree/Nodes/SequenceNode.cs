namespace Utilities.BehaviourTree
{
    public sealed class SequenceNode : BehaviourNode
    {
        private BehaviourNode[] childNodes;
        private int nodeIndex;

        public SequenceNode(params BehaviourNode[] behaviourNodes)
        {
            childNodes = behaviourNodes;
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
                    Stop(false);
                }
                else if (currentNode.Status == NodeStatus.success)
                {
                    currentNode.Exit();
                    nodeIndex++;

                    if (nodeIndex >= childNodes.Length) Stop(true);
                    else childNodes[nodeIndex].Enter();
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