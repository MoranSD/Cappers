using UnityEngine;
using Utils.StateMachine;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitGoToIdlePositionState : UnitState, IEnterableState, IPayloadedEnterableState<Vector3>, IExitableState
    {
        private Vector3 idlePosition;

        public UnitGoToIdlePositionState(OldUnitController controller) : base(controller)
        {
        }

        public void Enter(Vector3 idlePosition)
        {
            this.idlePosition = idlePosition;
            Enter();
        }

        public void Enter()
        {
            controller.View.MovementView.SetDestination(idlePosition);
        }

        public void Exit()
        {
            if(controller.View.MovementView.HasDestination)
                controller.View.MovementView.Stop();
        }
    }
}
