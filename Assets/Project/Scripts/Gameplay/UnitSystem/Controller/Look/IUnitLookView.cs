using Utils;

namespace Gameplay.UnitSystem.Controller.Look
{
    public interface IUnitLookView
    {
        bool TryGetTargetsAround(float range, out IAttackTarget[] targets);
    }
}
