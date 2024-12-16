using System;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Ship.Fight.Hole
{
    public class ShipHoleView : MonoBehaviour, IShipHoleView, IInteractor
    {
        public bool IsInteractable => true;

        public event Action OnFix;
        public event Action OnInteracted;

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
            OnFix?.Invoke();
        }
    }
}
