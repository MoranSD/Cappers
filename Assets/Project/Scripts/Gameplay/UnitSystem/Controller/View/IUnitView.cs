using Gameplay.UnitSystem.Controller.Movement;

namespace Gameplay.UnitSystem.Controller.View
{
    public interface IUnitView
    {
        IUnitMovementView MovementView { get; }
    }
}
