using Gameplay.Game;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Ship.UnitControl.LifeTime
{
    public class ShipUnitExistenceControl
    {
        public IReadOnlyList<UnitController> ActiveUnits => units;

        private readonly GameState gameState;
        private readonly ShipUnitPlacement placement;
        private readonly IUnitFactory unitFactory;

        private List<UnitController> units;

        public ShipUnitExistenceControl(GameState gameState, ShipUnitPlacement placement, IUnitFactory unitFactory)
        {
            this.gameState = gameState;
            this.placement = placement;
            this.unitFactory = unitFactory;
        }

        public void Initialize()
        {
            units = new List<UnitController>();

            for (int i = 0; i < gameState.Units.Count; i++)
            {
                var unitData = gameState.Units[i];
                var unitPosition = placement.GetUnitIdlePosition(unitData.Id);

                var unit = unitFactory.Create(unitData, unitPosition);
                units.Add(unit);
            }
        }

        public void Dispose()
        {

        }

        private void RemoveUnit(int unitId)
        {
            if (gameState.Units.Any(x => x.Id == unitId) == false)
                throw new System.Exception(unitId.ToString());

            var unitData = gameState.Units.First(x => x.Id == unitId);
            gameState.Units.Remove(unitData);
        }
    }
}
