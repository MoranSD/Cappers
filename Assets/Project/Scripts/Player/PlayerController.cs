using Infrastructure.TickManagement;
using Player.Data;
using Player.View;
using Services.GameInput;

namespace Player
{
    public class PlayerController : ITickable
    {
        private readonly PlayerConfig config;
        private readonly IPlayerView view;
        private readonly IInput input;

        public PlayerController(PlayerConfig config, IPlayerView view, IInput input)
        {
            this.config = config;
            this.view = view;
            this.input = input;
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            view.MovementView.Move(input.MoveInput, config.MovementConfig.Speed * deltaTime);
        }

        public void Dispose()
        {

        }
    }
}
