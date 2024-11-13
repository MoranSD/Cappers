using Gameplay.Game;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;
using Gameplay.UnitSystem.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Ship.UnitControl.LifeTime
{
    public class ShipUnitExistenceControl
    {
        public IReadOnlyList<IUnitController> ActiveUnits => units;

        private readonly GameState gameState;
        private readonly ShipUnitPlacement placement;
        private readonly IUnitFactory unitFactory;

        private List<IUnitController> units;

        public ShipUnitExistenceControl(GameState gameState, ShipUnitPlacement placement, IUnitFactory unitFactory)
        {
            this.gameState = gameState;
            this.placement = placement;
            this.unitFactory = unitFactory;
        }

        public void Initialize()
        {
            units = new List<IUnitController>();

            for (int i = 0; i < gameState.Units.Count; i++)
            {
                var unitData = gameState.Units[i];
                var unitPosition = placement.GetUnitIdlePosition(unitData.Id);

                var unit = unitFactory.Create(unitData, unitPosition);
                units.Add(unit);
            }
        }

        public void RemoveUnit(UnitData unitData)
        {
            if (gameState.Units.Any(x => x.Id == unitData.Id) == false)
                throw new System.Exception(unitData.ToString());

            gameState.Units.Remove(unitData);
            //remove from ActiveUnits list
        }
    }
}
