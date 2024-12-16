using Gameplay.UnitSystem.Controller;
using UnityEngine;

namespace Utils.Interaction
{
    public interface IUnitInteractable : IInteractor
    {
        Vector3 Position { get; }
        void Interact(UnitController unit);
    }
}
