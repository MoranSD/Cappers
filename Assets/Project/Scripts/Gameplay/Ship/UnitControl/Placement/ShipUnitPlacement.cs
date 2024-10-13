using Gameplay.Game;
using Gameplay.Ship.Data;
using Gameplay.Ship.UnitControl.Placement.View;
using System.Linq;

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

        public bool HasPlaceForUnit() => GetNextUnitId() != -1;

        public void AddUnit()
        {
            if (HasPlaceForUnit() == false)
                throw new System.Exception();

            //добавить данные юнита в gameState
            //установить новый id для юнита
            //установить задачу юниту "иди на idle позицию"
        }

        //для смерти юнита
        public void RemoveUnit(int unitId)
        {
            if(gameState.Units.Any(x => x.Id == unitId) == false)
                throw new System.Exception(unitId.ToString());

            var unitData = gameState.Units.First(x => x.Id == unitId);
            gameState.Units.Remove(unitData);
        }
    }
}
