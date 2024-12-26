using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;

namespace Utils.Interaction
{
    public interface IUnitInteractable : IInteractor
    {
        event Action<IUnitController> OnUnitInteracted;
        Transform Pivot { get; }
        void Interact(IUnitController unit);
    }
}
