using UnityEngine;

namespace Utilities.BehaviourTree
{
    public sealed class RandomDelayNode : BehaviourNode
    {
        private float initialSeconds;
        private float initialAdditionalRandomSeconds;

        private float delayDuration;
        private float delayTime;

        public RandomDelayNode(float seconds, float addtionalRandomSeconds)
        {
            initialSeconds = seconds;
            initialAdditionalRandomSeconds = addtionalRandomSeconds;
        }

        protected override void OnEnter()
        {
            //этого рандома тут не должно быть, тк это юнити, но системный не работает с float, а я хз как заставить
            delayDuration = initialSeconds + Random.Range(0, initialAdditionalRandomSeconds);
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