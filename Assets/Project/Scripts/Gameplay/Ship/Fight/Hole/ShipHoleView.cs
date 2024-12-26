using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Ship.Fight.Hole
{
    public class ShipHoleView : MonoBehaviour, IShipHoleView, IUnitInteractable
    {
        public bool IsInteractable => true;

        public Transform Pivot => transform;

        public event Action OnInteracted;
        public event Action<IUnitController> OnUnitInteracted;

        public void DrawDamage()
        {
            //всплеск воды
        }

        public void Hide()
        {
            Destroy(gameObject);
        }

        public void Interact()
        {
            OnInteracted?.Invoke();
        }

        public void Interact(IUnitController unit)
        {
            OnUnitInteracted?.Invoke(unit);
        }
    }
}
