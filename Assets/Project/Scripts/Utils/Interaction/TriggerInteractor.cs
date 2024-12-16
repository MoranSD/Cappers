using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class TriggerInteractor : MonoBehaviour, IInteractor
    {
        public event Action OnInteracted;
        public bool IsInteractable { get; set; } = true;

        public void Interact()
        {
            if (IsInteractable == false) return;

            OnInteracted?.Invoke();
        }
    }
}
