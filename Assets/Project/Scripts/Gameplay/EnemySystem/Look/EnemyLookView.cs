using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.EnemySystem.Look
{
    public class EnemyLookView : MonoBehaviour, IEnemyLookView
    {
        [SerializeField] private LayerMask targetMask;

        public bool TryGetTargetAround(float range, out IAttackTarget attackTarget)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetMask);

            attackTarget = colliders
                .Where(x => x.TryGetComponent(out IAttackTargetView attackTargetView) && attackTargetView.Target.IsDead == false)
                .Select(x => x.GetComponent<IAttackTargetView>().Target)
                .OrderBy(x => Vector3.Distance(x.GetPosition(), transform.position))
                .FirstOrDefault();

            return attackTarget != null;
        }
    }
}
