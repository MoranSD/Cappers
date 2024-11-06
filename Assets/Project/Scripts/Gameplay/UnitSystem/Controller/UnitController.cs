using Gameplay.Components.Health;
using Gameplay.UnitSystem.Controller.Command;
using Gameplay.UnitSystem.Controller.Command.Data;
using Gameplay.UnitSystem.Controller.View;
using Gameplay.UnitSystem.Data;
using Infrastructure.TickManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities.BehaviourTree;
using Utils;
using Utils.Interaction;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : ITickable, IAttackTarget
    {
        public UnitData Data { get; private set; }
        public IUnitView View { get; private set; }
        public HealthComponent Health { get; private set; }

        private BehaviourTreeRoot behaviourTreeRoot;
        private BehaviourTreeBlackBoard blackBoard;

        public UnitController(UnitData data, IUnitView view)
        {
            Data = data;
            View = view;
            behaviourTreeRoot = new BehaviourTreeRoot();
            blackBoard = new BehaviourTreeBlackBoard();
            Health = new(view.HealthView, 1);
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

        public void FollowPlayer(Transform followPoint)
        {
            blackBoard.SetValue("FollowPoint", followPoint);
            var commandActions = UnitCommandHolder.GetCommandActions(UnitCommandType.FollowPlayer, blackBoard);
            behaviourTreeRoot.ChangeBehaviour(commandActions);
        }

        public void StopFollowPlayer()
        {
            //TODO: go to idle position
            /*
             * еще как варик, будет же некий контроллер юнитов, а следовательно он будет знать, занят юнит или нет
             * и вот если он увидит что у юнита нет дел, он либо скажет ему идти на его айдл позицию, либо по делам
             */
            behaviourTreeRoot.ChangeBehaviour(null);
        }

        public void InteractWith(IUnitInteractable interactor)
        {
            interactor.Interact();
            behaviourTreeRoot.ChangeBehaviour(null);
        }

        public Vector3 GetPosition() => View.MovementView.GetPosition();

        public void ApplyDamage(float damage) => Health.ApplyDamage(damage);
    }
}
