using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Player.Look
{
    public class PlayerLook : MonoBehaviour, IPlayerLook
    {
        [SerializeField] private LayerMask interactorLayer;

        public bool TryGetInteractor(float range, out IInteractor interactor)
        {
            var interactors = Physics.OverlapSphere(transform.position, range, interactorLayer);

            for (int i = 0; i < interactors.Length; i++)
            {
                if (interactors[i].TryGetComponent(out interactor))
                    return true;
            }

            interactor = null;
            return false;
        }
    }
}
