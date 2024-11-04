using Gameplay.UnitSystem.Controller.Behaviour;
using Gameplay.UnitSystem.Controller.Command.Data;
using Utilities.BehaviourTree;

namespace Gameplay.UnitSystem.Controller.Command
{
    public class UnitCommandHolder
    {
        public static BehaviourNode GetCommandActions(UnitCommandType commandType, BehaviourTreeBlackBoard blackBoard)
        {
            switch (commandType)
            {
                case UnitCommandType.GoToPosition:
                    return new UnitMoveToNode(blackBoard);
            }

            throw new System.Exception();
        }
    }
}
