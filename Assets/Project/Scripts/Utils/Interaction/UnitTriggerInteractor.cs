using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class UnitTriggerInteractor : TriggerInteractor, IUnitInteractable
    {
        public event Action<IUnitController> OnUnitInteracted;

        public Vector3 Position => transform.position;

        public void Interact(IUnitController unit)
        {
            if(IsInteractable == false) return;

            OnUnitInteracted?.Invoke(unit);
        }
    }
}
