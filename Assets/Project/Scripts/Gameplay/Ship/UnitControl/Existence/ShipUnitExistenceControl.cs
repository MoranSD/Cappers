using Gameplay.Game;
using Gameplay.Ship.Data;
using Gameplay.Ship.UnitControl.View;
using Gameplay.UnitSystem.Buy.Data;
using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Ship.UnitControl
{
    public class ShipUnitExistenceControl
    {
        public IReadOnlyList<IUnitController> ActiveUnits => units;

        private readonly GameState gameState;
        private readonly ShipPlacementConfig config;
        private readonly IShipUnitExistenceView view;
        private readonly IUnitFactory unitFactory;

        private List<IUnitController> units;

        public ShipUnitExistenceControl(GameState gameState, ShipPlacementConfig config, IShipUnitExistenceView view, IUnitFactory unitFactory)
        {
            this.gameState = gameState;
            this.config = config;
            this.view = view;
            this.unitFactory = unitFactory;
        }

        public void Initialize()
        {
            units = new List<IUnitController>();

            for (int i = 0; i < gameState.Units.Count; i++)
            {
                var unitData = gameState.Units[i];
                var unitPosition = GetUnitIdlePosition(unitData.Id);

                var unit = unitFactory.Create(unitData, unitPosition);
                units.Add(unit);
            }
        }

        public void Dispose()
        {
            //тут раньше обновлялись данные в gameState и юнитах на сцене
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

        public void AddBoughtUnit(UnitToBuyData unitBuyData, Vector3 startPosition)
        {
            if (HasPlaceForUnit() == false)
                throw new System.Exception();

            var nextUnitId = GetNextUnitId();
            var unitData = unitBuyData.ToUnitData(nextUnitId);

            gameState.Units.Add(unitData);

            var unitController = unitFactory.Create(unitData, startPosition);
            unitController.GoToIdlePosition(GetUnitIdlePosition(unitData.Id));

            units.Add(unitController);
        }

        public void RemoveUnit(int unitId)
        {
            if (gameState.Units.Any(x => x.Id == unitId) == false)
                throw new System.Exception(unitId.ToString());

            gameState.Units.Remove(gameState.Units.First(x => x.Id == unitId));
            units.Remove(units.First(x => x.Id == unitId));
        }

        private int GetNextUnitId()
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
    }
}
