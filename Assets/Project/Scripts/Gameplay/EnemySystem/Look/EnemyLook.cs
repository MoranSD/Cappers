using UnityEngine;
using Utils;

namespace Gameplay.EnemySystem.Look
{
    public class EnemyLook : MonoBehaviour, IEnemyLook
    {
        [SerializeField] private LayerMask targetMask;

        public bool TryGetTargetAround(float range, out IAttackTarget attackTarget)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out attackTarget))
                    return true;
            }

            attackTarget = null;
            return false;
        }
    }
}
