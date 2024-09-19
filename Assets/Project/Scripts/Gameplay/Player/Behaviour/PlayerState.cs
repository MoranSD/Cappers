using Utils.StateMachine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerState : IState
    {
        protected readonly PlayerController controller;

        public PlayerState(PlayerController controller)
        {
            this.controller = controller;
        }
    }
}
