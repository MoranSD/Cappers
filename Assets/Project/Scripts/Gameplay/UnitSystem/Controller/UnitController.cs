using Gameplay.Components.Health;
using Gameplay.UnitSystem.Controller.Behaviour;
using Gameplay.UnitSystem.Controller.View;
using Gameplay.UnitSystem.Data;
using Infrastructure.TickManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using Utils.Interaction;
using Utils.StateMachine;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController : ITickable, IAttackTarget
    {
        public UnitData Data { get; private set; }
        public IUnitView View { get; private set; }
        public HealthComponent Health { get; private set; }
        public StateController StateController { get; private set; }

        public UnitController(UnitData data, IUnitView view)
        {
            Data = data;
            View = view;
            Health = new(view.HealthView, 1);
            StateController = new(
                new UnitGoToIdlePositionState(this),
                new UnitFollowPlayerState(this)
                );
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            StateController.UpdateCurrentState(deltaTime);
        }

        public void Dispose()
        {
            StateController.DisposeCurrent();
        }

        public void GoToIdlePosition(Vector3 position)
        {
            StateController.ChangeState<UnitGoToIdlePositionState, Vector3>(position);
        }

        public void FollowPlayer(Transform followPoint)
        {
            StateController.ChangeState<UnitFollowPlayerState, Transform>(followPoint);
        }

        public void StopFollowPlayer()
        {
            StateController.ChangeState<UnitGoToIdlePositionState>();
        }

        public void InteractWith(IUnitInteractable interactor)
        {
            interactor.Interact();
            StateController.ExitCurrent();
        }

        public Vector3 GetPosition() => View.MovementView.GetPosition();

        public void ApplyDamage(float damage) => Health.ApplyDamage(damage);
    }
}
