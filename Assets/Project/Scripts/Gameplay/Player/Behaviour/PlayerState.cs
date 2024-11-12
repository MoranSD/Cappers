using Utils.StateMachine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerState : IState
    {
        protected readonly OldPlayerController controller;

        public PlayerState(OldPlayerController controller)
        {
            this.controller = controller;
        }
    }
}
