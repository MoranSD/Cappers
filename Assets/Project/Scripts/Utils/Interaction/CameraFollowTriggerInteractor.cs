using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class CameraFollowTriggerInteractor : MonoBehaviour, IInteractor
    {
        public event Action OnInteracted;
        public bool IsInteractable => true;

        public void Interact() => OnInteracted?.Invoke();
    }
}
