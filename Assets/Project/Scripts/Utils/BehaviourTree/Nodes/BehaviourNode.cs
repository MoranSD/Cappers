namespace Utilities.BehaviourTree
{
    public abstract class BehaviourNode
    {
        public NodeStatus Status { get; private set; } = NodeStatus.idle;

        public void Enter()
        {
            if (Status != NodeStatus.idle) return;

            Status = NodeStatus.run;
            OnEnter();
        }

        public void Run(float deltaTime)
        {
            if (Status != NodeStatus.run) return;

            OnRun(deltaTime);
        }

        public void Exit()
        {
            if (Status == NodeStatus.idle) return;

            Status = NodeStatus.idle;
            OnExit();
        }

        protected void Stop(bool success)
        {
            if (Status != NodeStatus.run) return;

            Status = success ? NodeStatus.success : NodeStatus.failure;
        }

        protected virtual void OnEnter() { }
        protected virtual void OnRun(float deltaTime) { }
        protected virtual void OnExit() { }
    }
}