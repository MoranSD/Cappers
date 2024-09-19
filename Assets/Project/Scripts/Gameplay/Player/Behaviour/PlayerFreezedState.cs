using Utils.StateMachine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerFreezedState : PlayerState, IEnterableState
    {
        public PlayerFreezedState(PlayerController controller) : base(controller)
        {
        }

        public void Enter()
        {
            controller.Interaction.HandleInput = false;
            controller.Fight.HandleInput = false;
        }
    }
}
