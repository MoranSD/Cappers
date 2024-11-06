using UnityEngine;
using Utilities.BehaviourTree;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitFollowToPointNode : BehaviourNode
    {
        private readonly BehaviourTreeBlackBoard blackBoard;

        private Transform followPoint;
        private UnitController controller;

        private const float destinationUpdateRate = 0.5f;
        private float destinationUpdateTime = 0;

        public UnitFollowToPointNode(BehaviourTreeBlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        protected override void OnEnter()
        {
            followPoint = (Transform)blackBoard.GetValue("FollowPoint");
            controller = (UnitController)blackBoard.GetValue("Controller");
            controller.View.MovementView.SetDestination(followPoint.position);
            destinationUpdateTime = destinationUpdateRate;
        }

        protected override void OnRun(float deltaTime)
        {
            destinationUpdateTime -= deltaTime;

            if(destinationUpdateTime <= 0)
            {
                destinationUpdateTime = destinationUpdateRate;
                controller.View.MovementView.SetDestination(followPoint.position);
            }
        }

        protected override void OnExit()
        {
            var movement = controller.View.MovementView;

            if (movement.HasDestination)
                movement.Stop();
        }
    }
}
