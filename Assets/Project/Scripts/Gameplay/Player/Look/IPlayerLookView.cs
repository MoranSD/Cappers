using Utils;
using Utils.Interaction;

namespace Gameplay.Player.Look
{
    public interface IPlayerLookView
    {
        bool TryGetInteractor(float range, out IInteractor interactor);
        bool TryGetTargetsAround(float range, out IAttackTarget[] targets);
    }
}
