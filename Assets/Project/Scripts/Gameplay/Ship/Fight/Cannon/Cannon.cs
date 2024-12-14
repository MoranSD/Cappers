using Gameplay.Player.InteractController;
using Gameplay.SeaFight;
using Gameplay.SeaFight.Ship;
using Gameplay.Ship.Fight.Cannon.Data;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;

namespace Gameplay.Ship.Fight.Cannon
{
    public class Cannon : ITickable
    {
        public bool IsAvailable => reloadTime <= 0 && !isAiming && !isShooting;

        private float reloadTime;
        private bool isAiming;
        private bool isShooting;

        private readonly PlayerInteractController playerInteract;
        private readonly IInput input;
        private readonly SeaFightSystem seaFightSystem;
        private readonly ICannonView view;
        private float reloadDuration;

        public Cannon(PlayerInteractController playerInteract, IInput input, SeaFightSystem seaFightSystem, ICannonView view)
        {
            this.playerInteract = playerInteract;
            this.input = input;
            this.seaFightSystem = seaFightSystem;
            this.view = view;
            reloadDuration = 10;
        }

        public void Initialize(CannonInfo info)
        {
            seaFightSystem.EnemyShip.OnFightEnd += ExitInteraction;
            view.OnPlayerInteract += OnPlayerInteract;
            view.SetAvailable(true);
        }

        public void Dispose()
        {
            seaFightSystem.EnemyShip.OnFightEnd -= ExitInteraction;
            view.OnPlayerInteract -= OnPlayerInteract;
        }

        public void Update(float deltaTime)
        {
            if(reloadTime > 0)
            {
                reloadTime -= deltaTime;

                if(reloadTime <= 0)
                    view.SetAvailable(true);
            }

            if (!isAiming) return;

            if (input.IsExitButtonPressed)
            {
                ExitInteraction();
                return;
            }

            if (input.IsMeleeAttackButtonPressed)
            {
                reloadDuration += reloadTime;
                view.SetAvailable(false);

                ExitInteraction();
                isShooting = true;
                seaFightSystem.EnemyShip.SetCriticalZonesActive(false);
                view.EndAim();

                view.DrawCannonFly(() =>
                {
                    isShooting = false;
                    seaFightSystem.EnemyShip.ApplyDamage(view.AimPivot, 10);
                });
            }
        }

        private void OnPlayerInteract()
        {
            if (isAiming)
                throw new System.Exception();

            if (reloadTime > 0)
                return;

            isAiming = true;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(true);
            view.BeginAim();

            playerInteract.EnterInteractState(view.AimPivot);
        }

        private void ExitInteraction()
        {
            if (isAiming == false) return;

            isAiming = false;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(false);
            view.EndAim();
            playerInteract.ExitInteractState();
        }
    }
}
