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

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out IAttackTargetView attackTargetView))
                {
                    attackTarget = attackTargetView.Target;
                    return true;
                }
            }

            attackTarget = null;
            return false;
        }
    }
}
