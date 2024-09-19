namespace Utilities.BehaviourTree
{
    public sealed class MoveToNode : BehaviourNode
    {
        private readonly BehaviourTreeRoot behaviourTree;
        private readonly BehaviourNode node;

        public MoveToNode(BehaviourTreeRoot behaviourTree, BehaviourNode node)
        {
            this.behaviourTree = behaviourTree;
            this.node = node;
        }

        protected override void OnEnter()
        {
            behaviourTree.MoveToNode(node);
        }
    }
}