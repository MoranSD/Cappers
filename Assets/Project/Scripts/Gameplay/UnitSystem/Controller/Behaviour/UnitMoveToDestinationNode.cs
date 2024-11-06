using UnityEngine;
using Utilities.BehaviourTree;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitMoveToDestinationNode : BehaviourNode
    {
        private readonly BehaviourTreeBlackBoard blackBoard;
        private UnitController controller;

        public UnitMoveToDestinationNode(BehaviourTreeBlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        protected override void OnEnter()
        {
            var destination = (Vector3)blackBoard.GetValue("Destination");
            controller = (UnitController)blackBoard.GetValue("Controller");
            controller.View.MovementView.SetDestination(destination);
        }

        protected override void OnRun(float deltaTime)
        {
            var movement = controller.View.MovementView;

            if(movement.HasDestination == false || movement.RemainingDistance <= 0.1f)
                Stop(true);
        }

        protected override void OnExit()
        {
            var movement = controller.View.MovementView;

            if(movement.HasDestination)
                movement.Stop();
        }
    }
}
