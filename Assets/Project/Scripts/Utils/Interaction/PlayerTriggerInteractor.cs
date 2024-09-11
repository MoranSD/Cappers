using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class PlayerTriggerInteractor : MonoBehaviour, IInteractor
    {
        public event Action OnInteracted;

        public bool IsInteractable => isPlayerInTrigger;

        [SerializeField] private LayerMask playerLayer;

        private bool isPlayerInTrigger;

        public void Interact()
        {
            if (isPlayerInTrigger == false)
                return;

            OnInteracted?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isPlayerInTrigger) return;

            isPlayerInTrigger = ((1 << other.gameObject.layer) & playerLayer) != 0;
        }

        private void OnTriggerExit(Collider other)
        {
            if(isPlayerInTrigger == false) return;

            bool isPlayer = ((1 << other.gameObject.layer) & playerLayer) != 0;
            if (isPlayer == false) return;

            isPlayerInTrigger = false;
        }
    }
}
