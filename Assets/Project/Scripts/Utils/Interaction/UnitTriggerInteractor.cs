using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class UnitTriggerInteractor : TriggerInteractor, IUnitInteractable
    {
        public event Action<IUnitController> OnUnitInteracted;

        public Transform Pivot => transform;

        public void Interact(IUnitController unit)
        {
            if(IsInteractable == false) return;

            OnUnitInteracted?.Invoke(unit);
        }
    }
}
