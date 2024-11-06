using Gameplay.UnitSystem.Controller;
using System.Collections.Generic;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Player.Interact
{
    public class PlayerInteraction
    {
        public bool HandleInput;

        private readonly PlayerController controller;

        private List<UnitController> controlableUnits;

        public PlayerInteraction(PlayerController controller)
        {
            this.controller = controller;
            controlableUnits = new();
        }

        public void Initialize()
        {
            controller.Input.OnPressInteractButton += OnInteract;
        }

        public void Dispose()
        {
            controller.Input.OnPressInteractButton -= OnInteract;
        }

        private void OnInteract()
        {
            if (HandleInput == false) return;

            if (TryInteractWithInteractor())
                return;

            TryInteractWithUnit();
        }

        private void TryInteractWithUnit()
        {
            var playerLook = controller.View.Look;
            float interactRange = controller.Config.LookConfig.InteractRange;
            bool hasUnitsInRange = playerLook.TryGetUnitsAround(interactRange, out var units);

            if (hasUnitsInRange)
            {
                var closestUnit = GetClosestUnit(units);

                if (controlableUnits.Contains(closestUnit))
                {
                    controlableUnits.Remove(closestUnit);
                    closestUnit.StopFollowPlayer();
                    return;
                }

                controlableUnits.Add(closestUnit);
                closestUnit.FollowPlayer(controller.View.UnitFollowPivot);
            }
        }
        private bool TryInteractWithInteractor()
        {
            var playerLook = controller.View.Look;
            float interactRange = controller.Config.LookConfig.InteractRange;
            bool hasInteractorInRange = playerLook.TryGetInteractor(interactRange, out var interactor);

            if (hasInteractorInRange && interactor.IsInteractable)
            {
                if (controlableUnits.Count > 0 && interactor is IUnitInteractable)
                {
                    var unitToInteract = controlableUnits[0];
                    controlableUnits.RemoveAt(0);
                    unitToInteract.InteractWith(interactor as IUnitInteractable);
                }
                else
                {
                    interactor.Interact();
                }

                return true;
            }

            return false;
        }

        private UnitController GetClosestUnit(UnitController[] units)
        {
            var controllerPosition = controller.GetPosition();
            var closest = units[0];

            foreach (var current in units)
            {
                var currentPosition = current.GetPosition();
                var closestPosition = closest.GetPosition();

                if(Vector3.Distance(currentPosition, controllerPosition) < Vector3.Distance(closestPosition, controllerPosition))
                    closest = current;
            }

            return closest;
        }
    }
}
