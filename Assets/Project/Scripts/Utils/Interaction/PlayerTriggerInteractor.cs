using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class PlayerTriggerInteractor : MonoBehaviour, IInteractor, ICameraFollowInteractor
    {
        public event Action OnInteracted;

        public bool IsInteractable => isPlayerInTrigger;

        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Transform cameraPivot;

        private bool isPlayerInTrigger;

        public void Interact()
        {
            if (isPlayerInTrigger == false)
                return;

            OnInteracted?.Invoke();
        }

        public Vector3 GetCameraPivot() => cameraPivot.position;

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
