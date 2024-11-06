using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.UnitSystem.Controller.Look
{
    public class UnitLookView : MonoBehaviour, IUnitLookView
    {
        [SerializeField] private LayerMask targetLayer;

        public bool TryGetTargetsAround(float range, out IAttackTarget[] targets)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetLayer);

            targets = colliders
                .Where(x => x.TryGetComponent(out IAttackTargetView attackTargetView) && attackTargetView.Target.IsDead == false)
                .Select(x => x.GetComponent<IAttackTargetView>().Target)
                .ToArray();

            return targets.Length > 0;
        }
    }
}
