using Gameplay.Components.Health;
using Gameplay.UnitSystem.Controller.Look;
using Gameplay.UnitSystem.Controller.Movement;

namespace Gameplay.UnitSystem.Controller.View
{
    public interface IUnitView
    {
        UnitController Controller { get; }
        IUnitMovementView MovementView { get; }
        IHealthView HealthView { get; }
        IUnitLookView LookView { get; }
    }
}
