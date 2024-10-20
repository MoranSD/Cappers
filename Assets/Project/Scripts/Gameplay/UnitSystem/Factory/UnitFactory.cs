using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;

namespace Gameplay.UnitSystem.Factory
{
    public class UnitFactory : IUnitFactory
    {
        public UnitController Create(UnitData unitData)
        {
            return new UnitController(unitData);
        }
    }
}
