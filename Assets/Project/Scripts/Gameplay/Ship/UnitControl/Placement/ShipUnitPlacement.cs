using Gameplay.Game;
using Gameplay.Ship.Data;
using Gameplay.Ship.UnitControl.Placement.View;
using Gameplay.UnitSystem.Controller;
using System.Linq;
using UnityEngine;

namespace Gameplay.Ship.UnitControl.Placement
{
    public class ShipUnitPlacement
    {
        private readonly IShipUnitPlacementView view;
        private readonly GameState gameState;
        private readonly ShipPlacementConfig config;

        public ShipUnitPlacement(IShipUnitPlacementView view, GameState gameState, ShipPlacementConfig config)
        {
            this.view = view;
            this.gameState = gameState;
            this.config = config;
        }

        public int GetNextUnitId()
        {
            if (gameState.Units.Count > config.MaxUnitsCount)
                throw new System.Exception(gameState.Units.Count.ToString());

            for (int i = 0; i < config.MaxUnitsCount; i++)
            {
                if (gameState.Units.Any(x => x.Id == i)) continue;

                return i;
            }

            return -1;
        }

        public bool HasPlaceForUnit()
        {
            if (gameState.Units.Count > config.MaxUnitsCount)
                return false;

            for (int i = 0; i < config.MaxUnitsCount; i++)
            {
                if (gameState.Units.Any(x => x.Id == i)) continue;

                return true;
            }

            return false;
        }

        public Vector3 GetUnitIdlePosition(int unitId) => view.GetUnitPositions()[unitId];

        public void AddUnit(OldUnitController unitController)
        {
            if (HasPlaceForUnit() == false)
                throw new System.Exception();

            if (unitController.Data.Id != GetNextUnitId())
                throw new System.Exception(unitController.Data.ToString());

            gameState.Units.Add(unitController.Data);

            unitController.GoToIdlePosition(GetUnitIdlePosition(unitController.Data.Id));
        }
    }
}
