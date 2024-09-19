using Gameplay.CameraSystem;
using Infrastructure.TickManagement;
using Gameplay.Player.Data;
using Gameplay.Player.View;
using Infrastructure.GameInput;
using Gameplay.Player.Movement;
using Gameplay.Player.Interact;
using Utils.StateMachine;
using Gameplay.Player.Behaviour;
using Gameplay.Player.Fight;

namespace Gameplay.Player
{
    public class PlayerController : ITickable
    {
        public bool IsFreezed { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerInteraction Interaction { get; private set; }
        public PlayerFight Fight { get; private set; }

        public StateController StateController { get; private set; }

        public readonly PlayerConfig Config;
        public readonly IPlayerView View;
        public readonly IInput Input;
        public readonly IGameCamera GameCamera;

        public PlayerController(PlayerConfig config, IPlayerView view, IInput input, IGameCamera gameCamera)
        {
            IsFreezed = false;

            Config = config;
            View = view;
            Input = input;
            GameCamera = gameCamera;

            Movement = new(this);
            Interaction = new(this);
            Fight = new(this);

            StateController = new(
                new PlayerNormalState(this),
                new PlayerFreezedState(this)
                );
        }

        public void Initialize()
        {
            Interaction.Initialize();
            Fight.Initialize();

            StateController.ChangeState<PlayerNormalState>();
        }

        public void Dispose()
        {
            StateController.ExitCurrent();
            Interaction.Dispose();
            Fight.Dispose();
        }

        public void Update(float deltaTime)
        {
            StateController.UpdateCurrentState(deltaTime);
        }

        public void SetFreezee(bool freezee)
        {
            if(IsFreezed == freezee)
                throw new System.Exception(freezee.ToString());

            IsFreezed = freezee;

            if (IsFreezed) StateController.ChangeState<PlayerFreezedState>();
            else StateController.ChangeState<PlayerNormalState>();
        }
    }
}
