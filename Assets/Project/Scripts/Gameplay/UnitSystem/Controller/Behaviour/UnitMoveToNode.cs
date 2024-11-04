using UnityEngine;
using Utilities.BehaviourTree;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitMoveToNode : BehaviourNode
    {
        private readonly BehaviourTreeBlackBoard blackBoard;

        public UnitMoveToNode(BehaviourTreeBlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        protected override void OnEnter()
        {
            var destination = (Vector3)blackBoard.GetValue("Destination");
            var controller = (UnitController)blackBoard.GetValue("Controller");
            controller.View.MovementView.SetDestination(destination);
            Stop(true);
            //add distance to destination and stop node when its zero
        }
    }
}
