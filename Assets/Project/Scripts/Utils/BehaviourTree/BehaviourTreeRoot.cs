namespace Utilities.BehaviourTree
{
    public class BehaviourTreeRoot
    {
        public NodeStatus RootNodeStatus => rootNode.Status;

        public BehaviourTreeBlackBoard LocalBlackBoard { get; private set; }

        private BehaviourNode rootNode;

        public BehaviourTreeRoot(BehaviourNode rootNode, BehaviourTreeBlackBoard blackBoard = null)
        {
            if (rootNode == null)
                throw new System.Exception("root node shouldn't be null");

            LocalBlackBoard = blackBoard == null ? new() : blackBoard;

            this.rootNode = rootNode;
        }

        public void Initialize() => rootNode?.Enter();
        public void Run(float deltaTime) => rootNode?.Run(deltaTime);
        public void Dispose() => rootNode?.Exit();

        public void ChangeBehaviour(BehaviourNode newBehaviourNode)
        {
            rootNode?.Exit();
            rootNode = newBehaviourNode;
            rootNode?.Enter();
        }
    }
}