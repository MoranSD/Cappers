using Gameplay.UnitSystem.Controller.Command;
using Gameplay.UnitSystem.Controller.Command.Data;
using Gameplay.UnitSystem.Controller.View;
using Gameplay.UnitSystem.Data;
using Infrastructure.TickManagement;
using UnityEngine;
using Utilities.BehaviourTree;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : ITickable
    {
        public UnitData Data { get; private set; }
        public IUnitView View { get; private set; }

        private BehaviourTreeRoot behaviourTreeRoot;
        private BehaviourTreeBlackBoard blackBoard;

        public UnitController(UnitData data, IUnitView view)
        {
            Data = data;
            View = view;
            behaviourTreeRoot = new BehaviourTreeRoot();
            blackBoard = new BehaviourTreeBlackBoard();
        }

        public void Initialize()
        {
            blackBoard.SetValue("Controller", this);
        }

        public void Update(float deltaTime)
        {
            behaviourTreeRoot.Run(deltaTime);
        }

        public void Dispose()
        {
            behaviourTreeRoot.Dispose();
        }

        public void GoToPosition(Vector3 position)
        {
            blackBoard.SetValue("Destination", position);
            var commandActions = UnitCommandHolder.GetCommandActions(UnitCommandType.GoToPosition, blackBoard);
            behaviourTreeRoot.ChangeBehaviour(commandActions);
        }
    }
}
