using Gameplay.CameraSystem;
using Infrastructure.TickManagement;
using Gameplay.Player.Data;
using Gameplay.Player.View;
using Infrastructure.GameInput;
using Gameplay.Player.Interact;
using Utils.StateMachine;
using Gameplay.Player.Behaviour;
using Gameplay.Player.Fight;
using Gameplay.Components.Health;
using Utils;
using UnityEngine;

namespace Gameplay.Player
{
    public class OldPlayerController : ITickable, IAttackTarget
    {
        public bool IsFreezed { get; private set; }
        //public PlayerMovement Movement { get; private set; }
        public PlayerInteraction Interaction { get; private set; }
        public PlayerFight Fight { get; private set; }
        public HealthComponent Health { get; private set; }
        public bool IsDead => Health.Health <= 0;

        public StateController StateController { get; private set; }

        public readonly PlayerConfig Config;
        public readonly IPlayerView View;
        public readonly IInput Input;
        public readonly IGameCamera GameCamera;

        public OldPlayerController(PlayerConfig config, IPlayerView view, IInput input, IGameCamera gameCamera)
        {
            IsFreezed = false;

            Config = config;
            View = view;
            Input = input;
            GameCamera = gameCamera;

            //Movement = new(this);
            Interaction = new(this);
            Fight = new(this);
            Health = new(view.Health, config.HealthConfig.StartHealth);

            StateController = new(
                new PlayerNormalState(this),
                new PlayerFreezedState(this)
                );
        }

        public void Initialize()
        {
            Interaction.Initialize();
            Fight.Initialize();
            Health.OnDie += OnDie;

            StateController.ChangeState<PlayerNormalState>();
        }

        public void Dispose()
        {
            StateController.DisposeCurrent();
            Interaction.Dispose();
            Fight.Dispose();
            Health.OnDie -= OnDie;
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

        public Vector3 GetPosition() => Vector3.zero;//View.Movement.GetPosition();

        public void ApplyDamage(float damage) => Health.ApplyDamage(damage);

        private void OnDie()
        {
            StateController.ExitCurrent();
        }
    }
}
