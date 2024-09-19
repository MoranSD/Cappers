namespace Utilities.BehaviourTree
{
    public interface INodeWithChildren
    {
        bool Contains(BehaviourNode node);
        void MoveTo(BehaviourNode node);
    }
}