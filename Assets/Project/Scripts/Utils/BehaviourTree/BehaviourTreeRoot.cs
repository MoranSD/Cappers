namespace Utilities.BehaviourTree
{
    public class BehaviourTreeRoot
    {
        private BehaviourNode rootNode;

        public BehaviourTreeRoot(BehaviourNode rootNode)
        {
            if (rootNode == null)
                throw new System.Exception("root node shouldn't be null");

            this.rootNode = rootNode;
        }

        public void Initialize() => rootNode?.Enter();
        public void Run(float deltaTime)
        {
            if (rootNode.Status != NodeStatus.run) return;

            rootNode.Run(deltaTime);
        }
        public void Dispose() => rootNode?.Exit();

        public void MoveToNode(BehaviourNode node)
        {
            if(rootNode == node)
            {
                rootNode.Exit();
                rootNode.Enter();
            }
            else
            {
                if (rootNode is INodeWithChildren nodeWithChildren && nodeWithChildren.Contains(node))
                {
                    rootNode.Exit();
                    nodeWithChildren.MoveTo(node);
                }
                else
                {
                    throw new System.Exception($"BT doesnot contain node {node.GetType().ToString()}");
                }
            }
        }

        public void ChangeBehaviour(BehaviourNode newBehaviourNode)
        {
            rootNode?.Exit();
            rootNode = newBehaviourNode;
            rootNode?.Enter();
        }
    }
}