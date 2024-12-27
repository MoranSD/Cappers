using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Player.InteractController;
using Gameplay.SeaFight;
using Gameplay.Ship.Fight.Cannon.Data;
using Gameplay.UnitSystem;
using Gameplay.UnitSystem.Controller;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;
using System;
using System.Threading;
using Utils;

namespace Gameplay.Ship.Fight.Cannon
{
    public class Cannon : ITickable
    {
        public event Action OnUsed;
        public bool IsAvailable => reloadTime <= 0 && !isAiming && !isShooting && !IsUnitInteracting && seaFightSystem.IsInFight;
        public bool IsUnitInteracting { get; private set; }

        public readonly ICannonView View;

        private float reloadTime;
        private bool isAiming;
        private bool isShooting;
        private int interactUnitId = -1;

        private readonly PlayerInteractController playerInteract;
        private readonly IInput input;
        private readonly SeaFightSystem seaFightSystem;
        private float reloadDuration;

        private CancellationTokenSource cancellationTokenSource;

        public Cannon(PlayerInteractController playerInteract, IInput input, SeaFightSystem seaFightSystem, ICannonView view)
        {
            this.playerInteract = playerInteract;
            this.input = input;
            this.seaFightSystem = seaFightSystem;
            this.View = view;
            reloadDuration = 10;
        }

        public void Initialize(CannonInfo info)
        {
            cancellationTokenSource = new();

            EventBus.Subscribe<PlayerDieEvent>(OnPlayerDie);
            EventBus.Subscribe<UnitDieEvent>(OnUnitDie);
            seaFightSystem.EnemyShip.OnFightEnd += ExitPlayerInteraction;
            View.OnInteracted += OnPlayerInteract;
            View.OnUnitInteracted += OnUnitInteract;
            View.SetAvailable(true);
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            EventBus.Unsubscribe<PlayerDieEvent>(OnPlayerDie);
            EventBus.Unsubscribe<UnitDieEvent>(OnUnitDie);
            seaFightSystem.EnemyShip.OnFightEnd -= ExitPlayerInteraction;
            View.OnInteracted -= OnPlayerInteract;
            View.OnUnitInteracted -= OnUnitInteract;
        }

        public void Update(float deltaTime)
        {
            if(reloadTime > 0)
            {
                reloadTime -= deltaTime;

                if(reloadTime <= 0)
                    View.SetAvailable(true);
            }

            if (!isAiming) return;

            if (input.IsExitButtonPressed)
            {
                ExitPlayerInteraction();
                return;
            }

            if (input.IsMeleeAttackButtonPressed)
            {
                reloadDuration += reloadTime;
                View.SetAvailable(false);

                ExitPlayerInteraction();
                isShooting = true;
                seaFightSystem.EnemyShip.SetCriticalZonesActive(false);
                View.EndAim();

                View.DrawCannonFly(() =>
                {
                    isShooting = false;
                    seaFightSystem.EnemyShip.ApplyDamage(View.AimPivot, 5);
                });
            }
        }

        private void OnPlayerInteract()
        {
            if (IsAvailable == false)
                throw new System.Exception();

            isAiming = true;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(true);
            View.BeginAim();

            playerInteract.EnterInteractState(View.AimPivot);

            OnUsed?.Invoke();
        }

        private async void OnUnitInteract(IUnitController unit)
        {
            if (IsAvailable == false)
                throw new System.Exception();

            IsUnitInteracting = true;
            interactUnitId = unit.Id;
            unit.BeginCannonInteract(View.UnitInteractPivot);
            View.OnUnitAim();

            //duration depends on speed (base duration / unit.Speed)
            await UniTask.Delay(2000, false, PlayerLoopTiming.Update, cancellationTokenSource.Token);

            if (IsUnitInteracting == false) return;

            IsUnitInteracting = false;
            interactUnitId = -1;
            unit.EndCannonInteract();
            isShooting = true;
            View.DrawCannonFly(() =>
            {
                isShooting = false;
                seaFightSystem.EnemyShip.ApplyDamage(View.AimPivot, 0);
            });

            OnUsed?.Invoke();
        }

        private void ExitPlayerInteraction()
        {
            if (isAiming == false) return;

            isAiming = false;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(false);
            View.EndAim();
            playerInteract.ExitInteractState();
        }

        private void OnPlayerDie(PlayerDieEvent dieEvent) => ExitPlayerInteraction();

        private void OnUnitDie(UnitDieEvent dieEvent)
        {
            if (IsUnitInteracting == false) return;
            if (interactUnitId != dieEvent.UnitId) return;


            IsUnitInteracting = false;
            interactUnitId = -1;

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new();
        }
    }
}
