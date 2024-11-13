using Gameplay.UnitSystem.Controller;
using Utils;
using Utils.Interaction;

namespace Gameplay.Player.Look
{
    public interface IPlayerLookView
    {
        bool TryGetInteractor(float range, out IInteractor interactor);
        bool TryGetUnitsAround(float range, out OldUnitController[] units);
        bool TryGetTargetsAround(float range, out IAttackTarget[] targets);
    }
}
