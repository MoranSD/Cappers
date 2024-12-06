using Gameplay.UnitSystem.Controller;

namespace Utils.Interaction
{
    public interface IUnitInteractable : IInteractor
    {
        void Interact(UnitController unit);
    }
}
