using System.Linq;
using UnityEngine;
using Utils;
using Utils.Interaction;

namespace Gameplay.Player.Look
{
    public class PlayerLookView : MonoBehaviour, IPlayerLookView
    {
        [SerializeField] private LayerMask interactorLayer;
        [SerializeField] private LayerMask targetLayer;

        public bool TryGetInteractor(float range, out IInteractor interactor)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, interactorLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out interactor))
                    return true;
            }

            interactor = null;
            return false;
        }

        public bool TryGetTargetsAround(float range, out IAttackTarget[] targets)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetLayer);

            targets = colliders
                .Where(x => x.GetComponent<IAttackTarget>() != null)
                .Select(x => x.GetComponent<IAttackTarget>())
                .ToArray();

            return targets.Length > 0;
        }
    }
}
