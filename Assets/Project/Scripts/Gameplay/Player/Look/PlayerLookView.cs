using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Controller.View;
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
        [SerializeField] private LayerMask unitLayer;

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
                .Where(x => x.GetComponent<IAttackTargetView>() != null)
                .Select(x => x.GetComponent<IAttackTargetView>().Target)
                .ToArray();

            return targets.Length > 0;
        }

        public bool TryGetUnitsAround(float range, out UnitController[] units)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, unitLayer);

            units = colliders
                .Where(x => x.GetComponent<IUnitView>() != null)
                .Select(x => x.GetComponent<IUnitView>().Controller)
                .ToArray();

            return units.Length > 0;
        }
    }
}
