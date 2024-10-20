using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;

namespace Gameplay.UnitSystem.Factory
{
    public interface IUnitFactory
    {
        UnitController Create(UnitData unitData);
    }
}
