using UnityEngine;
using Utils.StateMachine;

namespace Gameplay.UnitSystem.Controller.Behaviour
{
    public class UnitFollowPlayerState : UnitState, IEnterableState, IPayloadedEnterableState<Transform>, IUpdateableState, IExitableState
    {
        private Transform playerPivot;
        private const float updateDestinationRate = 1f;
        private float updateDestinationTime;

        public UnitFollowPlayerState(UnitController controller) : base(controller)
        {
        }

        public void Enter(Transform playerPivot)
        {
            this.playerPivot = playerPivot;
            Enter();
        }

        public void Enter()
        {
            updateDestinationTime = updateDestinationRate;
        }

        public void Update(float deltaTime)
        {
            updateDestinationTime += deltaTime;
            if(updateDestinationTime >= updateDestinationRate)
            {
                updateDestinationTime = 0;
                controller.View.MovementView.SetDestination(playerPivot.position);
            }
        }

        public void Exit()
        {
            if (controller.View.MovementView.HasDestination)
                controller.View.MovementView.Stop();
        }

    }
}
