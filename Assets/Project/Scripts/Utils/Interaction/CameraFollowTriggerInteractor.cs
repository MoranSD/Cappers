using System;
using UnityEngine;

namespace Utils.Interaction
{
    public class CameraFollowTriggerInteractor : MonoBehaviour, IInteractor, ICameraFollowInteractor
    {
        public event Action OnInteracted;
        public bool IsInteractable => true;

        [SerializeField] private Transform cameraPivot;

        public void Interact() => OnInteracted?.Invoke();
        public Vector3 GetCameraPivot() => cameraPivot.position;
    }
}
