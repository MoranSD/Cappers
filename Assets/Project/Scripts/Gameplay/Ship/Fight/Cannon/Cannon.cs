using Gameplay.Player.InteractController;
using Gameplay.SeaFight;
using Gameplay.Ship.Fight.Cannon.Data;
using Gameplay.UnitSystem.Controller;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;

namespace Gameplay.Ship.Fight.Cannon
{
    public class Cannon : ITickable
    {
        public bool IsAvailable => reloadTime <= 0 && !isAiming && !isShooting && !isUnitInteracting;

        private float reloadTime;
        private bool isAiming;
        private bool isShooting;
        private bool isUnitInteracting;

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
            seaFightSystem.EnemyShip.OnFightEnd += ExitPlayerInteraction;
            view.OnPlayerInteract += OnPlayerInteract;
            view.OnUnitInteract += OnUnitInteract;
            view.SetAvailable(true);
        }

        public void Dispose()
        {
            seaFightSystem.EnemyShip.OnFightEnd -= ExitPlayerInteraction;
            view.OnPlayerInteract -= OnPlayerInteract;
            view.OnUnitInteract -= OnUnitInteract;
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
                ExitPlayerInteraction();
                return;
            }

            if (input.IsMeleeAttackButtonPressed)
            {
                reloadDuration += reloadTime;
                view.SetAvailable(false);

                ExitPlayerInteraction();
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
            if (IsAvailable == false)
                throw new System.Exception();

            isAiming = true;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(true);
            view.BeginAim();

            playerInteract.EnterInteractState(view.AimPivot);
        }

        private void OnUnitInteract(IUnitController unit)
        {
            if (IsAvailable == false)
                throw new System.Exception();

            isUnitInteracting = true;
            unit.BeginCannonInteract(view.UnitInteractPivot);
            view.OnUnitAim();

            //todo: await interact duration considering unit existence (dead or not)

            unit.EndCannonInteract();
            isUnitInteracting = false;
            isShooting = true;
            view.DrawCannonFly(() =>
            {
                isShooting = false;
                seaFightSystem.EnemyShip.ApplyDamage(view.AimPivot, 10);
            });
        }

        private void ExitPlayerInteraction()
        {
            if (isAiming == false) return;

            isAiming = false;
            seaFightSystem.EnemyShip.SetCriticalZonesActive(false);
            view.EndAim();
            playerInteract.ExitInteractState();
        }
    }
}
