namespace Utilities.BehaviourTree
{
    public sealed class DelayNode : BehaviourNode
    {
        private float delayDuration;
        private float delayTime;

        public DelayNode(float seconds)
        {
            delayDuration = seconds;
        }

        protected override void OnEnter()
        {
            delayTime = 0;
        }

        protected override void OnRun(float deltaTime)
        {
            delayTime += deltaTime;

            if (delayTime >= delayDuration)
                Stop(true);
        }
    }
}