using Utils.StateMachine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerNormalState : PlayerState, IEnterableState, IUpdateableState
    {
        public PlayerNormalState(OldPlayerController controller) : base(controller)
        {
        }

        public void Enter()
        {
            controller.Interaction.HandleInput = true;
            controller.Fight.HandleInput = true;
        }

        public void Update(float deltaTime)
        {
            //controller.Movement.Update(deltaTime);
            controller.Fight.Update(deltaTime);
        }
    }
}
